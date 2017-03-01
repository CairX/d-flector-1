using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Projectile_Type_Splitter : CS_Projectile_Type
{
    public override void SpecialCollision(Collision2D collision)
    {
        Instantiate(gameObject);
        Instantiate(gameObject);
        Destroy(gameObject);
    }
}
