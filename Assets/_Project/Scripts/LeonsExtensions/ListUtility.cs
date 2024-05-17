using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.LeonsExtensions
{
    public static class ListUtility
    {
        public static T GetRandom<T>(this List<T> list, bool remove = false)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("List is null or empty.");
                return default(T);
            }

            var randomIndex = UnityEngine.Random.Range(0, list.Count);
            T randomElement = list[randomIndex];

            if (remove)
            {
                list.RemoveAt(randomIndex);
            }

            return randomElement;
        }
        
        public static void Shuffle<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("List is null or empty.");
                return;
            }

            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}