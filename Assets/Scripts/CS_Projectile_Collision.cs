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

    private int healthPointIndex = -1;
    public List<ProjectileHealthPoint> healthPoints = new List<ProjectileHealthPoint>();

    private Owner owner = Owner.Enemy;

    public GameObject avatarVisuals;
    public GameObject avatarParticles;

    public GameObject enemyVisuals;
    public GameObject enemyParticles;

    public GameObject stickyParent;

    private float collisionTimer;
    private CS_Projectile_Type projectileType;
    private CS_Projectile_Movement movement;

    private Rigidbody2D rb;
    private float previousAngle = 0.0f;
    private IEnumerator previousAngleEnumerator;

    void Start ()
    {
        collisionTimer = COLLISION_COOLDOWN;

        projectileType = GetComponent<CS_Projectile_Type>();
        movement = GetComponent<CS_Projectile_Movement>();
        rb = GetComponent<Rigidbody2D>();

        ChangeOwner(owner);
    }

    private void OnEnable()
    {
        if (previousAngleEnumerator == null)
        {
            previousAngleEnumerator = PreviousAngle();
        }
        StartCoroutine(previousAngleEnumerator);
    }

    private void OnDisable()
    {
        StopCoroutine(previousAngleEnumerator);
        StopAllCoroutines();
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
        FireCollisionParticle(collision);

        if (projectileType && isAlive())
        {
            projectileType.SpecialCollision(collision);
        }

        if (isActiveAndEnabled)
        {
            StartCoroutine(previousAngleEnumerator);
        }
    }

    private void UpdateHealth()
    {
        healthPointIndex++;
        if (isAlive())
        {
            avatarVisuals.GetComponent<SpriteRenderer>().sprite = healthPoints[healthPointIndex].avatar;
            enemyVisuals.GetComponent<SpriteRenderer>().sprite = healthPoints[healthPointIndex].enemy;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool isAlive() {
        return (healthPointIndex < healthPoints.Count);
    }

    private void FireCollisionParticle(Collision2D collision)
    {
        Quaternion quaternion = new Quaternion();
        quaternion.eulerAngles = new Vector3(0, 0, previousAngle);

        GameObject prefab;
        if (isAvatar())
        {
            prefab = avatarParticles;
        } else
        {
            prefab = enemyParticles;
        }

        GameObject pararticles = Instantiate(prefab, collision.contacts[0].point, quaternion, transform.parent);
        ParticleSystem par = pararticles.GetComponent<ParticleSystem>();
        Destroy(pararticles, par.main.duration);
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

    private IEnumerator PreviousAngle()
    {
        yield return new WaitForEndOfFrame();
        previousAngle = CS_Utils.PointToDegree(rb.velocity);
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
