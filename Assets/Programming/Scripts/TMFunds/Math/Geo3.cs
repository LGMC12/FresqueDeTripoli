using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.Math
{
    public static class Geo3
    {
        public static Vector3 Orbital2Cart(Vector3 pivot, float yaw, float pitch, float radius = 1f)
        {
            float cosP = Mathf.Cos(pitch);
            return new Vector3(pivot.x + cosP * Mathf.Cos(yaw) * radius,
                pivot.y + Mathf.Sin(pitch) * radius,
                pivot.z + cosP * Mathf.Sin(yaw) * radius
                );
        }
    }
}


