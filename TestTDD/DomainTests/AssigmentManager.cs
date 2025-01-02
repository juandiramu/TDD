using TestTDD.ApplicationTests.Services._Resources.Constants;
using TestTDD.ApplicationTests.Services._Resources;
using TestTDD.ApplicationTests.Services.Validations;

namespace TestTDD.DomainTests;

public class AssigmentManager
{
	private readonly List<Assigment> _assigments = new();

	public Assigment CreateAssigment(string name, string description)
	{
		var assigment = Assigment.Create(name, description);
		_assigments.Add(assigment);
		return assigment;
	}

	public Assigment UpdateAssigment(Assigment assigment)
	{
		Assigment editAssigment = _assigments.FirstOrDefault(it => it.Id == assigment.Id) ?? new();
		AssigmentsValidations.ValidateNotNullAssigment(editAssigment);
		editAssigment.Edit(assigment.Name, assigment.Description, assigment.State);
		return editAssigment;
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
	public void DeleteAssigment(Guid id)
	{
		Assigment task = _assigments.FirstOrDefault(t => t.Id == id) ?? new();
		AssigmentsValidations.ValidateNotNullAssigment(task);
		_assigments.Remove(task);
	}
}

