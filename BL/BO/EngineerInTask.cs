namespace BO;

/// <summary>
/// logical auxiliary entity: engineer in task
/// </summary>
public class EngineerInTask
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public override string ToString() => this.ToStringProperty();

}
