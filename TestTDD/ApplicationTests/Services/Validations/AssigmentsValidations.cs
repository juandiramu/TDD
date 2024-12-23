using TestTDD.ApplicationTests.Services._Resources;
using TestTDD.ApplicationTests.Services._Resources.Constants;

namespace TestTDD.ApplicationTests.Services.Validations
{
	public class AssigmentsValidations
	{
		public static void ValidateNotNullOrEmpty(string propertyValue, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyValue))
			{
				throw new AssigmentException(string.Format(AssigmentsMessages.NotNullProperty,propertyName), AssigmentsConstants.ERROR_CODE_500);
			}
		}
	}
}
