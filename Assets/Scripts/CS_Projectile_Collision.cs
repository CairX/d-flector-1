using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Collision : MonoBehaviour
{

    public enum Owner
    {
        Avatar,
        Enemy
    }

    private static float COLLISION_COOLDOWN = 0.16f;

    private int healthPointIndex = 0;
    public List<ProjectileHealthPoint> healthPoints = new List<ProjectileHealthPoint>();

    private Owner owner = Owner.Enemy;

    public GameObject avatarVisuals;
    public GameObject avatarParticle;
    public GameObject enemyVisuals;
    public GameObject enemyParticle;

    public GameObject stickyParent;

    private float collisionTimer;
    private CS_Projectile_Type projectileType;
    private CS_Projectile_Movement movement;

    void Start ()
    {
        collisionTimer = COLLISION_COOLDOWN;

        projectileType = GetComponent<CS_Projectile_Type>();
        movement = GetComponent<CS_Projectile_Movement>();

        ChangeOwner(owner);
    }


    private void Update()
    {
        collisionTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prevent collisions from triggering to frequantly,
        // instantly killing the projectile. This often happens
        // when a player pushed a projectile with the shield.
        // The timer cooldown value is entirely arbitary.
        if (collisionTimer > 0) { return; }
        collisionTimer = COLLISION_COOLDOWN;

        UpdateHealth();
        RouteOnCollisionEnter2D(collision);

        //GameObject paro = Instantiate(avatarParticle, transform.position, transform.rotation);
        Rigidbody2D trb = GetComponent<Rigidbody2D>();
        Quaternion qr = Quaternion.LookRotation(trb.velocity);
        qr.eulerAngles = new Vector3(0, 0, CS_Utils.PointToDegree(trb.velocity * -1));
        float angle = CS_Utils.PointToDegree(collision.contacts[0].normal);
        Debug.Log(qr.eulerAngles);
        //Quaternion q = Quaternion.LookRotation();
        //GameObject paro = Instantiate(avatarParticle, collision.contacts[0].point, new Quaternion());
        Quaternion q = transform.rotation;
        GameObject paro = Instantiate(avatarParticle, collision.contacts[0].point, qr);
        ParticleSystem par = paro.GetComponent<ParticleSystem>();
        par.Play();

        Destroy(paro, par.main.duration);
        //Debug.Log(avatarParticle);
        //Transform part = enemyVisuals.transform.GetChild(1);
        //part.Rotate(transform.rotation.eulerAngles);
        //GetComponent<Rigidbody2D>().velocity.normalized * -1;
        //part.RotateAround(transform.position, Vector3.forward, CS_Utils.PointToDegree(GetComponent<Rigidbody2D>().velocity.normalized));
        //part.GetComponent<ParticleSystem>().Play();
        //avatarParticle.Play();
        //avatarParticle.Emit(100);

        if (projectileType && healthPointIndex > 0)
        {
            projectileType.SpecialCollision(collision);
        }
    }

    private void UpdateHealth()
    {
        healthPointIndex++;
        if (healthPointIndex >= healthPoints.Count)
        {
            Destroy(gameObject);
        }
        else
        {
            avatarVisuals.GetComponent<SpriteRenderer>().sprite = healthPoints[healthPointIndex].avatar;
            enemyVisuals.GetComponent<SpriteRenderer>().sprite = healthPoints[healthPointIndex].enemy;
        }
    }

    private void RouteOnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                OnEnemyCollisionEnter2D(collision);
                break;
            case "Player":
                OnPlayerCollisionEnter2D(collision);
                break;
            case "Shield":
                OnShieldCollisionEnter2D(collision);
                break;
            case "net":
                OnNetCollisionEnter2D(collision);
                break;
            case "StickySheild":
                onStickySheildCollisionEnter2D(collision);
                break;
            default:
                break;
        }
    }

    private void OnNetCollisionEnter2D(Collision2D collision)
    {
        CS_All_Audio.Instance.NetBonce();
    }

    private void OnEnemyCollisionEnter2D(Collision2D collision)
    {
        if (isAvatar())
        {
            Destroy(gameObject);
        }
    }

    private void OnPlayerCollisionEnter2D(Collision2D collision)
    {
        if (isEnemy())
        {
            Destroy(gameObject);
        }
    }

    private void OnShieldCollisionEnter2D(Collision2D collision)
    {
        if (owner == Owner.Enemy)
        {
            ChangeOwner(Owner.Avatar);
        }

        CS_All_Audio.Instance.ProjectileVsShield();
    }

    private void onStickySheildCollisionEnter2D(Collision2D collision)
    {
        movement.Stick();
        this.transform.SetParent(collision.transform);
        
        if (owner == Owner.Enemy)
        {
            ChangeOwner(Owner.Avatar);
        }

    }

    public bool isAvatar()
    {
        return (owner == Owner.Avatar);
    }

    public bool isEnemy()
    {
        return (owner == Owner.Enemy);
    }

    public void ChangeOwner(Owner owner)
    {
        this.owner = owner;
        switch (this.owner)
        {
            case Owner.Avatar:
                gameObject.layer = LayerMask.NameToLayer("ProjectileAvatar");
                avatarVisuals.SetActive(true);
                enemyVisuals.SetActive(false);
                break;
            case Owner.Enemy:
                gameObject.layer = LayerMask.NameToLayer("ProjectileEnemy");
                avatarVisuals.SetActive(false);
                enemyVisuals.SetActive(true);
                break;
            default:
                break;
        }
    }

    public Owner GetOwner()
    {
        return owner;
    }
}

[System.Serializable]
public class ProjectileHealthPoint
{
    public Sprite avatar;
    public Sprite enemy;
}
