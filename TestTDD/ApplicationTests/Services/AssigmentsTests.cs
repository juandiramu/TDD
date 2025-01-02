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
	public void CreateAssigments_ShouldGenerateUniqueIds()
	{
		// Arrange & Act
		Assigment assigment1 = Assigment.Create("Task 1", "Description 1");
		Assigment assigment2 = Assigment.Create("Task 2", "Description 2");

		// Assert
		Assert.NotEqual(assigment1.Id, assigment2.Id);
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
	public void CreateAssigment_WithEmptyDescription_ShouldError()
	{
		// Act & Assert
		var exception = Assert.Throws<AssigmentException>(() => AssigmentsValidations.ValidateNotNullOrEmpty(string.Empty, nameof(Assigment.Description)));
		Assert.Equal(AssigmentsConstants.ERROR_CODE_500, exception.ErrorCode);
		Assert.Equal(string.Format(AssigmentsMessages.NotNullProperty, nameof(Assigment.Description)), exception.Message);
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
	[Fact]
	public void GetAllAssigments_WhenNoTasksExist_ShouldReturnEmptyList()
	{
		// Arrange
		AssigmentManager assigmantManager = new AssigmentManager();

		// Act
		var tasks = assigmantManager.GetAssigments();

		// Assert
		Assert.NotNull(tasks);
		Assert.Empty(tasks);
	}

	[Fact]
	public void UpdateAssigment_WhenTaskExists_ShouldUpdateDetails()
	{
		// Arrange
		AssigmentManager assigmantManager = new();
		var createdTask = assigmantManager.CreateAssigment("Task 1", "Description 1");

		// Act
		var updatedTask = new Assigment
		{
			Id = createdTask.Id,
			Name = "Updated Task",
			Description = "Updated Description",
			State = AssigmentState.InProgress
		};
		var result = assigmantManager.UpdateAssigment(updatedTask);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Updated Task", result.Name);
		Assert.Equal("Updated Description", result.Description);
		Assert.Equal(AssigmentState.InProgress, result.State);
	}

	[Fact]
	public void UpdateAssigment_WhenTaskDoesNotExist_ShouldThrowException()
	{
		// Arrange
		AssigmentManager assigmantManager = new();
		var nonExistentTask = AssigmentTestModel.nonExistentTask;

		// Act & Assert
		var exception = Assert.Throws<AssigmentException>(() => assigmantManager.UpdateAssigment(nonExistentTask));
		Assert.Equal(AssigmentsConstants.ERROR_CODE_404, exception.ErrorCode);
		Assert.Equal(AssigmentsMessages.TaskNotFound, exception.Message);
	}

	[Fact]
	public void DeleteAssigment_WhenTaskExists_ShouldRemoveTask()
	{
		// Arrange
		AssigmentManager assigmantManager = new();
		var createdTask = assigmantManager.CreateAssigment("Task 1", "Description 1");

		// Act
		assigmantManager.DeleteAssigment(createdTask.Id);

		// Assert
		var tasks = assigmantManager.GetAssigments();
		Assert.Empty(tasks);
	}

	[Fact]
	public void DeleteAssigment_WhenTaskDoesNotExist_ShouldThrowException()
	{
		// Arrange
		AssigmentManager assigmantManager = new();
		var nonExistentId = Guid.NewGuid();

		// Act & Assert
		var exception = Assert.Throws<AssigmentException>(() => assigmantManager.DeleteAssigment(nonExistentId));
		Assert.Equal(AssigmentsConstants.ERROR_CODE_404, exception.ErrorCode);
		Assert.Equal(string.Format(AssigmentsMessages.TaskNotFound, nonExistentId), exception.Message);
	}


}
