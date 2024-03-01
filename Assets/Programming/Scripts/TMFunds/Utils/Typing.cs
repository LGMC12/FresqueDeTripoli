using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds
{
    public static class Typing
    {
        public static int Bool2Int(bool value)
        {
            return value ? 1 : 0;
        }

        public static float Bool2Float(bool value)
        {
            return value ? 1f : 0f;
        }

        public static bool Float2Bool(float value)
        {
            return value > 0;
        }

        public static bool Int2Bool(int value)
        {
            return value > 0;
        }

        public static string Int2String(int value)
        {
            return value.ToString();
        }

        public static string Float2String(float value)
        {
            return value.ToString();
        }
    }
}


