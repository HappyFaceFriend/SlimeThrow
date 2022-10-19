using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Random
    {
        /// <summary>
        /// Shuffles the list with Fisher-Yates algorithm
        /// </summary>
        /// <param name="list">List to shuffle.</param>
        public static void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i >= 1; i--)
            {
                int r = UnityEngine.Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[r];
                list[r] = temp;
            }
        }
        public static int RandomIndex<T>(List<T> list)
        {
            return UnityEngine.Random.Range(0, list.Count);
        }
        public static T RandomElement<T>(List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        public static Vector2 RandomRange(float minX, float maxX, float minY, float maxY)
        {
            return new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
        }
        public static Vector3 RandomRange(Vector3 minVec, Vector3 maxVec)
        {
            return new Vector3(UnityEngine.Random.Range(minVec.x, maxVec.x),
                UnityEngine.Random.Range(minVec.y, maxVec.y), UnityEngine.Random.Range(minVec.z, maxVec.z));
        }
    }

}