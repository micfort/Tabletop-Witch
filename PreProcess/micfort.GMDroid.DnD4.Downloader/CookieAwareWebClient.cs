﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace micfort.GMDroid.DnD4.Downloader
{
	public class CookieAwareWebClient : WebClient
	{
		public void Login(string loginPageAddress, NameValueCollection loginData)
		{
			CookieContainer container;

			var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			var buffer = Encoding.ASCII.GetBytes(loginData.ToString());
			request.ContentLength = buffer.Length;
			var requestStream = request.GetRequestStream();
			requestStream.Write(buffer, 0, buffer.Length);
			requestStream.Close();

			container = request.CookieContainer = new CookieContainer();

			var response = request.GetResponse();
			string responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
			Console.Out.WriteLine("responseText = {0}", responseText);
			response.Close();
			CookieContainer = container;
		}

		public CookieAwareWebClient(CookieContainer container)
		{
			CookieContainer = container;
		}

		public CookieAwareWebClient()
			: this(new CookieContainer())
		{ }

		public CookieContainer CookieContainer { get; private set; }

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = (HttpWebRequest)base.GetWebRequest(address);
			request.CookieContainer = CookieContainer;
			return request;
		}
	}

}
