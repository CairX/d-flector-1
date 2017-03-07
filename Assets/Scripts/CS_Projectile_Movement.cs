using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Movement : MonoBehaviour
{
    // Arbitary force multiplier, that exists simply to move the speed value down.
    public static int FORCE = 100;

    private Rigidbody2D rb;
    private Vector2 direction;
    private bool speedup = false;

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

    void Update()
    {
        if ((CS_WorldManager.Instance.slowdown != 1  && speedup == false) ||
            (CS_WorldManager.Instance.slowdown == 1 && speedup == true))
        {
            speedup = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * ((speed * FORCE) / CS_WorldManager.Instance.slowdown));

            if(CS_WorldManager.Instance.slowdown == 1)
            {
                speedup = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        UpdateRotation(CS_Utils.PointToDegree(rb.velocity.normalized));
    }

    // Not currently in use. Alternative to bounce material.
    private void Bounce(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        float contactAngle = CS_Utils.GetCollisionAngle(transform, contact.point);

        // Scope between intervals to make sure that it matches perfectly.
        contactAngle = CS_Utils.AngleRound(contactAngle);
        // Transform "motion" into "collision" angle using 90 offset.
        float offset = CS_Utils.Mod(contactAngle + 90, 360);

        // Reset angle to be based on zero.
        float newAngle = angle - offset;
        // Invert in order to reflect.
        newAngle *= -1;
        // Get reflection angle.
        newAngle = offset + newAngle;
        // Rotate 180 degrees to travle in opposite direction.
        newAngle = CS_Utils.Mod(newAngle + 180, 360);

        UpdateRotation(newAngle);
    }

    public void UpdateRotation(float a)
    {
        angle = a;
        direction = CS_Utils.DegreeToPoint(a);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, a - 90);
        rb.velocity = direction * (speed / CS_WorldManager.Instance.slowdown);
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
