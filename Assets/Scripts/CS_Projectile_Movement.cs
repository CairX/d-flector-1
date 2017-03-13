using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;

    [Range(0, 360)]
    public float angle;
    public float speed;

    private float stickyAngle;
    private float stickySpeed;
    private Vector2 stickyDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        CS_Notifications.Instance.Register(this, "Relese");

        UpdateRotation(angle);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        UpdateRotation(CS_Utils.PointToDegree(rb.velocity.normalized));
    }

    public void UpdateRotation(float a)
    {
        angle = a;
        direction = CS_Utils.DegreeToPoint(a);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, a - 90);
        rb.velocity = direction * speed;
    }

    public void Stick()
    {
        stickyAngle = angle;
        stickySpeed = speed;
        stickyDirection = direction;

        rb.Sleep();
    }

    public void Relese()
    {
        if (rb.IsSleeping())
        {
            rb.WakeUp();
            angle = stickyAngle;
            speed = stickySpeed;
            direction = stickyDirection;
            UpdateRotation(angle);
        }
    }
}
