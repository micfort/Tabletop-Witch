using System;

namespace micfort.GMDroid.PF.TypeConverter
{
	public class BoolConverter: ITypeConverter
	{
		public BoolConverter()
		{
		}

		public Newtonsoft.Json.Linq.JToken Convert(string data)
		{
			if(data == "0")
			{
				return false;
			}
			else if(data == "1")
			{
				return true;
			}
			else
			{
				throw new Exception();
			}
		}
	}
}

