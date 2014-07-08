using System;
using System.Net.Security;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Parse;
using System.Security.Cryptography.X509Certificates;

namespace micfort.GMDroid.ViewSync
{
	enum Action
	{
		upload,
		download,
		none,
	}
	class MainClass
	{
		private static Action action = Action.none;
		private static string workDirectory = "work";

		public static bool Validator (object sender, X509Certificate certificate, X509Chain chain, 
		                              SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public static void Main (string[] args)
		{
			ParseCommandLine (args);
			ServicePointManager.ServerCertificateValidationCallback = Validator;
			try
			{
				ParseClient.Initialize ("14Q2hQn42Q77RxuEb19PghEVKWPfsr6UdSJCKxjc", "Toe9NtPOVn1byUGm71rrBAQJqkSF9h2tb8dLJdvY");

				if (action == Action.download)
				{
					Task t = PullInformation ();
					t.ContinueWith ((str) =>
					{
						Console.WriteLine ("Finished");
					});
					t.Wait ();
				}
				else if (action == Action.upload)
				{
					Task t = PushInformation ();
					t.ContinueWith ((str) =>
					                {
						Console.WriteLine ("Finished");
					});
					t.Wait ();
				}
				else
				{

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine ("Exception: {0}", ex);
			}
		}

		private static async Task PullInformation()
		{
			var query = ParseObject.GetQuery ("SetItemClassification");

			IEnumerable<ParseObject> results = await query.FindAsync();

			if (Directory.Exists (workDirectory))
			{
				Directory.Delete (workDirectory, true);
			}
			Directory.CreateDirectory (workDirectory);

			foreach (ParseObject classification in results)
			{
				string classificationPath = workDirectory + Path.DirectorySeparatorChar + classification.ObjectId + "_" + classification.Get<string> ("name");
				Directory.CreateDirectory(classificationPath);
				IDictionary<string, string> viewer = classification.Get<IDictionary<string, string>> ("viewer");
				foreach (var view in viewer)
				{
					string viewPath = classificationPath + Path.DirectorySeparatorChar + view.Key + ".js";
					File.WriteAllText (viewPath, view.Value);
				}
			}
		}

		private static async Task PushInformation()
		{
			var query = ParseObject.GetQuery ("SetItemClassification");

			IEnumerable<ParseObject> results = await query.FindAsync();

			foreach (ParseObject classification in results)
			{
				string classificationPath = workDirectory + Path.DirectorySeparatorChar + classification.ObjectId + "_" + classification.Get<string> ("name");
				if (Directory.Exists (classificationPath))
				{
					IDictionary<string, string> OldViews = classification.Get<IDictionary<string, string>> ("viewer");

					List<string> views = new List<string>(Directory.GetFiles (classificationPath, "*.js"));
					foreach (var view in views)
					{
						string code = File.ReadAllText(view);
						string viewName = Path.GetFileNameWithoutExtension (view);
						OldViews [viewName] = code;
					}
					classification ["viewer"] = OldViews;
					await classification.SaveAsync ();
				}
			}
		}

		private static void ParseCommandLine(string[] args)
		{
			bool succes = true;
			if (args.Length == 0)
			{
				succes = false;
			}
			else
			{
				if (!Enum.TryParse (args [0], out action))
				{
					succes = false;
				}
			}

			if (!succes)
			{
				Console.WriteLine ("Usage: ViewSync <Action>");
			}
		}
	}
}
