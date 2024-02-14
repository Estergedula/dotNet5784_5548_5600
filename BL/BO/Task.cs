namespace BO;
/// <summary>
/// Task Entity represents a student with all its props
/// </summary>
/// <param name="Id">unique ID (created automatically)</param>
/// <param name="Description">Full description of the task</param>
/// <param name="Alias">Short name of the task</param>
/// <param name="Milestone">Boolean milestone of the task</param>
/// <param name="Status">The status of the task</param>
/// <param name="DependenciesList">A list of dependencies (task in list type)</param>
/// <param name="CreatedAt">Creation date of the task.</param>
/// <param name="ScheduleDate">Estimated start date</param>
/// <param name="Start">Actual start date of the task</param>
/// <param name="ForecastDate">Estimated date for completion of the task</param>
/// <param name="DeadLine">Last date for completing the task</param>
/// <param name="Complete">Actual assignment completion date</param>
/// <param name="Deliverables">A string describing the product</param>
/// <param name="Remarks">Notes on the task</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="ComplexilyLevel">The difficulty level of the task</param>
/// 
public class Task
{
    public required int Id { get; init; }
    public required string  Description { get; set; }
    public required string? Alias { get; set; }
    public MillestoneInTask? Milestone { get; set; }
    public Status Status { get; set; }
    public IEnumerable<TaskInList>? DependenciesList { get; set; }
    public required DateTime ?CreatedAt { get; set; }
    public DateTime ?ScheduleDate { get;  set; }
    public DateTime ?Start { get; set; }
    public DateTime ?ForecastDate { get; set; }//תאריך משוער לסיום
    public DateTime ?DeadLine { get; set; }//תאריך אחרון לסיום
    public DateTime ?Complete { get; set; }//תאריך סיום בפועל
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? EngineerId { get; set; }
    public EngineerExperience ComplexilyLevel { get; set; }

}

