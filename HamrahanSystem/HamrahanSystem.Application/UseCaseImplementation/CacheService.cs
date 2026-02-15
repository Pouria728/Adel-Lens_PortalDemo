using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.IdentityModel.Protocols;

namespace HamrahanSystem.Application.UseCaseImplementation
{
	public class CacheService : ICacheService
	{
		private  IDatabase _db;
		TimeSpan _defaultCacheDuration;
		public CacheService(string connectionString, TimeSpan defaultDuration)
		{
			if (defaultDuration <= TimeSpan.Zero)
				throw new ArgumentOutOfRangeException(nameof(defaultDuration), "Duration must be greater than zero");

			_db = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString)).Value.GetDatabase();
			_defaultCacheDuration = defaultDuration;
		}



		public T GetData<T>(string key)
		{
			var value = _db.StringGet(key);
			if (!string.IsNullOrEmpty(value))
			{
				return JsonConvert.DeserializeObject<T>(value);
			}
			return default;
		}
		public bool SetData<T>(string key, T value)
		{
			
			var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), _defaultCacheDuration);
			return isSet;
		}
		public object RemoveData(string key)
		{
			bool _isKeyExist = _db.KeyExists(key);
			if (_isKeyExist == true)
			{
				return _db.KeyDelete(key);
			}
			return false;
		}
	}
}