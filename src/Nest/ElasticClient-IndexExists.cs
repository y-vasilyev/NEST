﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using Nest.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using Nest.Domain;

namespace Nest
{
	public partial class ElasticClient
	{
		public IndexExistsResponse IndexExists(string index)
		{
			return this._IndexExists(index);
		}
		private IndexExistsResponse _IndexExists(string index)
		{
			var path = this.CreatePath(index);
			var status = this.Connection.HeadSync(path);
			var response = new IndexExistsResponse()
			{
				IsValid = false,
				Exists = false,
				ConnectionStatus = status
			};
			if (status.Error == null || status.Error.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
			{ 
				//404 is an expected possible status code for this call.
				response.IsValid = true;
			}
			if (status.Error == null)
			{ 
				response.Exists = true;
			}
			return response;
		}
	}
}
