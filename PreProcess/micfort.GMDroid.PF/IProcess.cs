using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace micfort.GMDroid.PF
{
	public interface IProcess
	{
		void LoadInput(string input);
		List<JObject> GenerateJson();
	}
}

