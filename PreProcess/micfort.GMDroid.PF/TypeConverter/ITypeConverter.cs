using System;
using Newtonsoft.Json.Linq;

namespace micfort.GMDroid.PF.TypeConverter
{
	public interface ITypeConverter
	{
		JToken Convert(string data);
	}
}

