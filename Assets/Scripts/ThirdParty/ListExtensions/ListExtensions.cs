using System;
using System.Collections.Generic;
using System.Linq;
using ThirdParty.ControlledRandom;

namespace ThirdParty.ListExtensions
{
    public static class ListExtension
    {
        public static T Random<T>(this List<T> list)
        {
            if (list.Count == 0)
                return default(T);

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T Random<T>(this List<T> list, IRandomService randomService)
        {
            if (list.Count == 0)
                return default(T);

            return list[randomService.Range(0, list.Count)];
        }

        public static T Random<T>(this IEnumerable<T> list)
        {
            return Random(list.ToList());
        }

        public static T Random<T>(this IEnumerable<T> list, IRandomService randomService)
        {
            return Random(list.ToList(), randomService);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static void Shuffle<T>(this IList<T> list, IRandomService randomService)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = randomService.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}