using TestTDD.ApplicationTests.Services._Resources;
using TestTDD.ApplicationTests.Services._Resources.Constants;
using TestTDD.DomainTests;

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

		public static void ValidateNotNullAssigment(Assigment? assigment)
		{
			if (assigment == null || string.IsNullOrEmpty(assigment.Name))
			{
				throw new AssigmentException(AssigmentsMessages.TaskNotFound, AssigmentsConstants.ERROR_CODE_404);
			}
		}
	}
}
