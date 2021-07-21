using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Extensions
{
    public static class ListExtension
    {
        private static readonly Random rng = new ();

        public static void Shuffle<T>(this IList<T> list)
        {
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
