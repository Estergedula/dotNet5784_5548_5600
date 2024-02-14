namespace BO;
/// <summary>
/// Primary logical entity of Task represents a student with all its props
/// </summary>
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
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperience ComplexilyLevel { get; set; }

}

