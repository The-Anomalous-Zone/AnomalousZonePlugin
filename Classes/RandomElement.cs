using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.Classes
{
    public static class RandomElement
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ElementAt(UnityEngine.Random.Range(0, enumerable.Count() -1));
        }
    }
}