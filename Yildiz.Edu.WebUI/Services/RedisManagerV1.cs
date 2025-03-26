using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Yildiz.Edu.WebUI.Services
{
    public class RedisManagerV1
    {
        private ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6370");
        private IDatabase db;
        public RedisManagerV1()
        {
            db = redis.GetDatabase(0);
        }

        public void Set<T>(string key, T value)
        {
            var jsonString = JsonConvert.SerializeObject(value, Formatting.Indented);
            db.StringSet(key, jsonString);
        }

        public T Get<T>(string key)
        {
            var cachedJsonString = db.StringGet(key);

            if (cachedJsonString.HasValue)
            {
                var cachedData = JsonConvert.DeserializeObject<T>(cachedJsonString);

                return cachedData;
            }

            return default(T);
        }

        public void Remove(string key)
        {
            db.KeyDelete(key);
        }
    }
}
