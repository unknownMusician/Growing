using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growing.Utils
{
    public static class CollectionsExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionaryFromTuple<TKey, TValue>(
            this IEnumerable<(TKey Key, TValue Value)> values
        )
        {
            return values.ToDictionary(static x => x.Key, static x => x.Value);
        }

        public static async Task<T[]> WaitSimultaneous<T>(this IEnumerable<Task<T>> tasks)
        {
            return await Task.WhenAll(tasks);
        }

        public static async Task<List<T>> WaitSequent<T>(this IEnumerable<Task<T>> tasks)
        {
            var results = new List<T>();

            foreach (Task<T> task in tasks)
            {
                results.Add(await task);
            }

            return results;
        }
    }
}
