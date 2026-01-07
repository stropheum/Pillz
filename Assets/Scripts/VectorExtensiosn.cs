using UnityEngine;

namespace Pillz
{
    public static class VectorExtensions
    {
        public static Vector3 ToXZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);    
        }
        
        public static Vector3 FlattenToXZ(this Vector3 value)
        {
            return new Vector3(value.x, 0, value.z);
        }
    }
}