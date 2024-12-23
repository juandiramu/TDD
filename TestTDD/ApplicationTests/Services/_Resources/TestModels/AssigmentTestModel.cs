using TestTDD.DomainTests;

namespace TestTDD.ApplicationTests.Services._Resources.TestModels
{
	public class AssigmentTestModel
	{
		public static readonly AssigmentManager assigments = new();
		public static readonly List<Assigment> assigmentsList = assigments.GetAssigments();
		public static readonly Assigment assigmentsImput = new() { Name = "Tarea1", Description = "Esta es la tarea1"};
		public static readonly Assigment assigmentsImput2 = new() { Name = "Tarea2", Description = "Esta es la tarea2" };
		public static readonly Assigment assigmentsImput3 = new() { Name = "Tarea3", Description = "Esta es la tarea3" };
		public static readonly Assigment assigmentsImput4 = new() { Name = "Tarea4", Description = "Esta es la tarea4" };
		public static AssigmentManager ReturnAssignments()
		{
			assigments.AddAssigment(assigmentsImput);
			assigments.AddAssigment(assigmentsImput2);
			assigments.AddAssigment(assigmentsImput3);
			assigments.AddAssigment(assigmentsImput4);
			return assigments;
		}
	}
}
