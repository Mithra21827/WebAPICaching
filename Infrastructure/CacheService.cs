using StackExchange.Redis;
using System.Text.Json;
using WebAPiCaching.Services;

namespace WebAPiCaching.Infrastructure
{
    public class CacheService : ICacheService
    {
        IDatabase _cacheDb;

        public CacheService() {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb = redis.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if(!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<T>(value);
            return default;
        }

        public object RemoveData(string key)
        {
            var _isexit = _cacheDb.KeyExists(key);
            if (_isexit)  
                return _cacheDb.KeyDelete(key);
            return false;
            
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationtime)
        {
            var expiryTime = expirationtime.DateTime.Subtract(DateTime.Now);
            var isSet = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
            return isSet; 
        }
    }
}
