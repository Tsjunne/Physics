using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    internal static class Extensions
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static IEnumerable<TOut> Merge<TIn, TOut>(this IEnumerable<TIn> collection1, IEnumerable<TIn> collection2, Func<TIn, TIn, TOut> aggregation)
        {
            var enumerator1 = collection1.GetEnumerator();
            var enumerator2 = collection2.GetEnumerator();

            var more1 = enumerator1.MoveNext();
            var more2 = enumerator2.MoveNext();

            while (more1 || more2)
            {
                yield return aggregation(
                    more1 ? enumerator1.Current : default(TIn), 
                    more2 ? enumerator2.Current : default(TIn));

                more1 = enumerator1.MoveNext();
                more2 = enumerator2.MoveNext();
            }
        }

        public static int Hash<T>(this IEnumerable<T> collection)
        {
            if (!collection.Any()) return 0;

            var hash = 17;

            foreach (var item in collection)
            {
                hash = hash * 23 + item.GetHashCode();
            }

            return hash;
        }
    }
}
