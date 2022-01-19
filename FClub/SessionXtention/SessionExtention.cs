using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FClub.SessionXtention
{
	public static class SessionExtention
	{
		public static void SetSession<T>(this ISession session, string key, T value)
		{

			session.SetString(key, JsonSerializer.Serialize(value));

		}
		public static T GetSession<T>(this ISession session, string key)
		{

			return JsonSerializer.Deserialize<T>(session.GetString(key)) ?? default;

		}
	}
}
