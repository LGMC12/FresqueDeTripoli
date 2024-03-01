using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.Math
{
    public static class Vec3
    {
        public static Vector3 New(float value)
        {
            return new Vector3(value, value, value);
        }

        public static Vector3 New(Quaternion value)
        {
            return value * Vector3.one;
        }
    }
}


