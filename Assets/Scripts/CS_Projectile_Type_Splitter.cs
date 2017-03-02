using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Type_Splitter : CS_Projectile_Type
{
    public override void SpecialCollision(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CS_Projectile_Collision pc = GetComponent<CS_Projectile_Collision>();

        float magnitude = rb.velocity.magnitude;
        float angle = CS_Utils.PointToDegree(rb.velocity);
        CS_Projectile_Collision.Owner owner = pc.GetOwner();

        float angle1 = (angle + 45);
        Vector2 velocity1 = CS_Utils.DegreeToPoint(angle1);
        CreateChild(velocity1, magnitude, owner);

        float angle2 = (angle - 45);
        Vector2 velocity2 = CS_Utils.DegreeToPoint(angle2);
        CreateChild(velocity2, magnitude, owner);

        Destroy(gameObject);
    }

    private void CreateChild(Vector2 velocity, float magnitude, CS_Projectile_Collision.Owner owner)
    {
        GameObject child = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
        Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
        CS_Projectile_Collision pc = child.GetComponent<CS_Projectile_Collision>();
        pc.ChangeOwner(owner);

        rb.velocity = velocity;
        child.transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
}
