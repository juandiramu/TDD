using Xunit.Sdk;

namespace TestTDD.ApplicationTests.Services._Resources
{
	public class AssigmentException : Exception
	{
		public int ErrorCode { get; }

		public AssigmentException(string message, int errorCode) : base(message)
		{
			ErrorCode = errorCode;
		}
	}
}
