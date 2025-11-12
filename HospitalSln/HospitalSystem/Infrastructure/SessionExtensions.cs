using System.Text.Json;

namespace HospitalSystem.Infrastructure
{
	public static class SessionExtensions
	{
		public static void SetJson<T>(this ISession session, string key, T value)
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}

		public static T? GetJson<T>(this ISession session, string key)
		{
			var data = session.GetString(key);
			return data == null ? default(T) : JsonSerializer.Deserialize<T>(data);
		}
	}
}
