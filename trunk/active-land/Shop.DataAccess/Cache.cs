using System.Collections.Concurrent;

namespace Shop.DataAccess
{
    public class Cache
    {
        public static readonly ConcurrentDictionary<string, object> Default = new ConcurrentDictionary<string, object>();
    }
}