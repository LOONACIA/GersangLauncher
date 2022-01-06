using System;

namespace GersangGameManager
{
	public class GameManagerException : Exception
	{
		public GameManagerException() : base()
		{

		}
		public GameManagerException(string message) : base(message)
		{

		}
	}
}
