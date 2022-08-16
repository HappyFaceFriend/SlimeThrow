using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Vectors
    {
        /// <summary>
        /// Returns a Vector3 (x,y,0)
        /// </summary>
        /// <param name="vector"></param>
        public static Vector3 Vec2ToVec3(Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }
    }
}
