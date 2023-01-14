using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GersangGameManager.Service
{
	public class WebSocketClosedException : Exception
	{
		public WebSocketClosedException(string? description)
			: base($"Connection closed. Description: {(description is null ? string.Empty : description)}")
		{
			Description = description;
		}

		public string? Description { get; }
	}

}