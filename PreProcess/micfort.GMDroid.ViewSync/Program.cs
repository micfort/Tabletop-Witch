using System;
using System.Net.Security;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Parse;
using micfort.GHL.Logging;


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
            micfort.GHL.GHLWindowsInit.Init();

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
                        ErrorReporting.Instance.ReportInfoT("main", "Finished");
					});
					t.Wait ();
				}
				else if (action == Action.upload)
				{
					Task t = PushInformation ();
					t.ContinueWith ((str) =>
					                {
                                        ErrorReporting.Instance.ReportInfoT("main", "Finished");
					});
					t.Wait ();
				}
				else
				{

				}
			}
			catch (Exception ex)
			{
                ErrorReporting.Instance.ReportFatalT("main", "Exception", ex);
			}
            Console.ReadKey();
		}

		private static async Task PullInformation()
		{
            ErrorReporting.Instance.ReportInfoT("main", "Getting all classifications");
			var query = ParseObject.GetQuery ("SetItemClassification");

			IEnumerable<ParseObject> results = await query.FindAsync();

            ErrorReporting.Instance.ReportInfoT("main", "Delete old directory");
			if (Directory.Exists (workDirectory))
			{
				Directory.Delete (workDirectory, true);
			}
			Directory.CreateDirectory (workDirectory);

			foreach (ParseObject classification in results)
			{
				ErrorReporting.Instance.ReportInfoT(classification.Get<string> ("name"), "Creating files");

				string classificationPath = workDirectory + Path.DirectorySeparatorChar + classification.ObjectId + "_" + classification.Get<string> ("name");
				Directory.CreateDirectory(classificationPath);

                IDictionary<string, string> viewer;
                if (classification.TryGetValue<IDictionary<string, string>>("viewer", out viewer))
                {
                    foreach (var view in viewer)
                    {
                        ErrorReporting.Instance.ReportInfoT(classification.Get<string>("name"), string.Format("Creating view {0}", view.Key));
                        string viewPath = classificationPath + Path.DirectorySeparatorChar + "view_" + view.Key + ".js";
                        File.WriteAllText(viewPath, view.Value);
                    }
                }
                else
                {
                    ErrorReporting.Instance.ReportWarnT(classification.Get<string>("name"), "No views defined");
                }
                
                IDictionary<string, string> editors;
                if (classification.TryGetValue<IDictionary<string, string>>("editor", out editors))
                {
                    foreach (var editor in editors)
                    {
                        ErrorReporting.Instance.ReportInfoT(classification.Get<string>("name"), string.Format("Creating editor {0}", editor.Key));
                        string editorPath = classificationPath + Path.DirectorySeparatorChar + "editor_" + editor.Key + ".js";
                        File.WriteAllText(editorPath, editor.Value);
                    }
                }
                else
                {
                    ErrorReporting.Instance.ReportWarnT(classification.Get<string>("name"), "No editors defined");
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
                    IDictionary<string, string> views, editors;
                    if (!classification.TryGetValue<IDictionary<string, string>>("viewer", out views))
                    {
                        ErrorReporting.Instance.ReportWarnT(classification.Get<string>("name"), "No views defined, creating new set");
                        views = new Dictionary<string, string>();
                    }
                    if (!classification.TryGetValue<IDictionary<string, string>>("editor", out editors))
                    {
                        ErrorReporting.Instance.ReportWarnT(classification.Get<string>("name"), "No editors defined, creating new set");
                        editors = new Dictionary<string, string>();
                    }

					List<string> files = new List<string>(Directory.GetFiles (classificationPath, "*.js"));
					foreach (var file in files)
					{
                        if (file.StartsWith("view"))
                        {
                            string code = File.ReadAllText(file);
                            string viewName = Path.GetFileNameWithoutExtension(file);
                            views[viewName] = code;
                        }
                        else if (file.StartsWith("editor"))
                        {
                            string code = File.ReadAllText(file);
                            string viewName = Path.GetFileNameWithoutExtension(file);
                            editors[viewName] = code;
                        }
					}
					classification["viewer"] = views;
                    classification["editor"] = editors;
                    ErrorReporting.Instance.ReportWarnT(classification.Get<string>("name"), "Saving to server");
					await classification.SaveAsync();
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

				if (args.Length == 2)
				{
					workDirectory = args [1];
				}
			}

			if (!succes)
			{
				Console.WriteLine ("Usage: ViewSync <Action> [work directory]");
				Console.WriteLine ("Action:");
				Console.WriteLine ("- download (download all the files, will first delete the whole directory)");
				Console.WriteLine ("- upload (upload all the files that are in the work directory)");
			}
		}
	}
}
