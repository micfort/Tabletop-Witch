using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace micfort.GMDroid.PF
{
	public class ParseFormatter
	{
		public ParseFormatter()
		{
		}

		public JObject GenerateParseJson(List<JObject> data, string set_, string classification)
		{
			JArray results = new JArray();
			for(int i = 0; i < data.Count; i++)
			{
				JObject item = new JObject();
				item["Description"] = data[i];
				item["Set"] = CreatePointer("Set", set_);
				item["classification"] = CreatePointer("SetItemClassification", classification);
				results.Add(item);
			}
			JObject output = new JObject();
			output["results"] = results;
			return output;
		}

		private JObject CreatePointer(string className, string id)
		{
			JObject output = new JObject();
			output["__type"] = "Pointer";
			output["className"] = className;
			output["objectId"] = id;
			return output;
		}
	}
}

