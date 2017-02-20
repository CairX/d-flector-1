using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Utils : MonoBehaviour {

    public static float Mod(float x, float m)
    {
        return (x % m + m) % m;
    }

    public static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public static float AngleRound(float a)
    {
        if ((a > 315 && a <= 360) || (a >= 0 && a <= 45))
        {
            return 0;
        }
        else if (a > 45 && a <= 135)
        {
            return 90;
        }
        else if (a > 135 && a <= 225)
        {
            return 180;
        }

        return 270;
    }

    public static Vector2 DegreeToPoint(float degree)
    {
        float radian = Mod(degree, 360) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static float PointToDegree(Vector2 point)
    {
        float degree = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        return Mod(degree, 360);
    }

    public static float GetCollisionAngle(Transform hitobjectTransform, Vector2 contactPoint)
    {
        Vector2 collidertWorldPosition = new Vector2(hitobjectTransform.position.x, hitobjectTransform.position.y);
        Vector3 pointB = contactPoint - collidertWorldPosition;

        float theta = Mathf.Atan2(pointB.x, pointB.y);
        float angle = (360 - ((theta * 180) / Mathf.PI)) % 360;
        return angle;
    }
}
