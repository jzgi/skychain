﻿using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Greatbone.Core
{
	///
	/// An event queue message
	///
	public class MsgEvent : IDisposable
	{
		private string topic;

		private string key;

		HttpResponseMessage msg;

		internal MsgEvent(HttpContext context)
		{

		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}