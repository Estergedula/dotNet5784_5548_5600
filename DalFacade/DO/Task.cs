namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Description"></param>
/// <param name="Alias"></param>
/// <param name="Milestone"></param>
/// <param name="CreatedAt"></param>
/// <param name="Start"></param>
/// <param name="ForecadtDate"></param>
/// <param name="DeadLine"></param>
/// <param name="Complete"></param>
/// <param name="Deliverables"></param>
/// <param name="Remarks"></param>
/// <param name="EngineerId"></param>
/// <param name="ComplexilyLevel"></param>
public record Task
(
    int Id,
    string Description,
    string Alias,
    bool Milestone,
    DateTime CreatedAt,
    DateTime Start,
    DateTime ForecadtDate,
    DateTime DeadLine,
    DateTime Complete,
    string ?Deliverables=null,
    string ?Remarks=null,
    int EngineerId=0,
    EngineerExperience ComplexilyLevel=EngineerExperience.Junior
);
