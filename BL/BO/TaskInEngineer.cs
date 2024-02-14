namespace BO;

/// <summary>
/// logical auxiliary entity: task in engineer
/// </summary>
public class TaskInEngineer
{
    public int Id { get; init; }
    public required string ?Alias { get; set; }
    public override string ToString() => this.ToStringProperty();

}
