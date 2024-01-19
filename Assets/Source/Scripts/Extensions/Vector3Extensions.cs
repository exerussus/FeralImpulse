using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class Vector3Extensions
    {
        public static bool GetIsCloseDistance(this Vector3 firstPosition, Vector3 secondPosition, float distance)
        {
            return Vector3.Distance(firstPosition, secondPosition) <= distance;
        }
    }
}