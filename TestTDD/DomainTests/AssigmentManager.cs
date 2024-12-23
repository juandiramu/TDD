using TestTDD.ApplicationTests.Services._Resources.Constants;
using TestTDD.ApplicationTests.Services._Resources;

namespace TestTDD.DomainTests
{
	public class AssigmentManager
	{
		private readonly List<Assigment> _assigments = new();

		public Assigment CreateAssigment(string name, string description)
		{
			var assigment = Assigment.Create(name, description);
			_assigments.Add(assigment);
			return assigment;
		}
		public Assigment AddAssigment(Assigment assigment)
		{
			_assigments.Add(assigment);
			return assigment;
		}

		public List<Assigment> GetAssigments()
		{
			return _assigments;
		}

		public Assigment GetAssigmentByName(string name)
		{
			Assigment? assigments = _assigments.FirstOrDefault(t => t.Name == name);
			if (assigments == null)
			{
				throw new AssigmentException(AssigmentsMessages.TaskNotFoundByName,
					AssigmentsConstants.ERROR_CODE_404
				);
			}
			return assigments;
		}
	}
}

