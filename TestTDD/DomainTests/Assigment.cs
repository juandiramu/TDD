namespace TestTDD.DomainTests;

public class Assigment
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public AssigmentState State { get; set; }

	public static Assigment Create(string name, string description)
		 => new() { Id = Guid.NewGuid(), Name = name, Description = description, State = AssigmentState.ToDo };

}
