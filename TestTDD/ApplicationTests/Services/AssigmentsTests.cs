using TestTDD.ApplicationTests.Services._Resources;
using TestTDD.ApplicationTests.Services._Resources.Constants;
using TestTDD.ApplicationTests.Services._Resources.TestModels;
using TestTDD.ApplicationTests.Services.Validations;
using TestTDD.DomainTests;

namespace TestTDD.Services.ApplicationTests;

//Las clases deben seguir la siguiente taxonomia: nombreClaseTest
public class AssigmentsTests
{
	[Theory]
	[InlineData(AssigmentsConstants.ASSIGMENT_NAME, AssigmentsConstants.ASSIGMENT_DESCRIPTION)]
	//Los nombres de los métodos deben seguir la siguiente Taxonomía: funcionalidad_casoPrueba_resultadoEsperado
	public void CreateAssigment_WithValidTitleAndDescription_ShouldSaveCorrectly(string name, string description)
	{
		//Arrange & Act
		Assigment assigment = Assigment.Create(name, description);

		//Assert
		Assert.True(assigment != null);
		Assert.True(!string.IsNullOrEmpty(assigment.Name));
		Assert.True(!string.IsNullOrEmpty(assigment.Description));
		Assert.True(assigment.State == AssigmentState.ToDo);
	}

	[Fact]
	public void CreateAssigment_WithNotEmptyId_ShouldSaveCorrectly()
	{
		//Arrange & Act
		Assigment assigment = Assigment.Create(string.Empty,string.Empty);

		//Assert
		Assert.True(assigment != null);
		Assert.True(!string.IsNullOrEmpty(assigment.Id.ToString()));
	}

	[Fact]
	public void CreateAssigment_WithEmptyName_ShouldError()
	{
		// Act & Assert
		var exception = Assert.Throws<AssigmentException>(() => AssigmentsValidations.ValidateNotNullOrEmpty(string.Empty, nameof(Assigment.Name)));
		Assert.Equal(AssigmentsConstants.ERROR_CODE_500, exception.ErrorCode); 
		Assert.Equal(string.Format(AssigmentsMessages.NotNullProperty, nameof(Assigment.Name)), exception.Message);
	}

	[Fact]
	public void GetAllAssigments_WhenTasksExist_ShouldReturnAllTasks()
	{
		// Arrange & Act
		List<Assigment> tasks = (AssigmentTestModel.ReturnAssignments()).GetAssigments();

		// Assert
		Assert.NotNull(tasks);
		Assert.Equal(4, tasks.Count);
		Assert.All(tasks, task =>
		{
			Assert.False(string.IsNullOrEmpty(task.Id.ToString()));
			Assert.False(string.IsNullOrEmpty(task.Name));
			Assert.False(string.IsNullOrEmpty(task.Description));
			Assert.False(string.IsNullOrEmpty(task.State.ToString()));
		});
	}

	[Fact]
	public void GetAssigmentByName_WhenTaskExists_ShouldReturnCorrectTask()
	{
		// Arrange
		AssigmentManager assigmantManager = AssigmentTestModel.ReturnAssignments();

		// Act
		Assigment result = assigmantManager.GetAssigmentByName("Tarea1");

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Tarea1", result.Name);
		Assert.Equal("Esta es la tarea1", result.Description);
	}

	[Fact]
	public void GetAssigmentByName_WhenTaskDoesNotExist_ShouldThrowException()
	{
		// Arrange
		AssigmentManager assigmantManager = AssigmentTestModel.ReturnAssignments();

		// Act & Assert
		var exception = Assert.Throws<AssigmentException>(() => assigmantManager.GetAssigmentByName("Nonexistent Task"));
		Assert.Equal(AssigmentsConstants.ERROR_CODE_404, exception.ErrorCode);
		Assert.Equal(AssigmentsMessages.TaskNotFoundByName, exception.Message);
	}

}
