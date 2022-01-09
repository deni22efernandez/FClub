
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FClub.CustomMapper
{
	public static class Mapper
	{
		//public static T Map<T>(this object value)
		//{
		//	return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));
		//}
		public static T Map<T>(this object value)
		{
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value));
		}
	}
}
