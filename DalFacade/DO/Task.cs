using System.Threading.Tasks;

namespace DO;

/// <summary>
/// Task Entity represents a student with all its props
/// </summary>
/// <param name="Id">unique ID (created automatically)</param>
/// <param name="Description">Full description of the task</param>
/// <param name="Alias">Short name of the task</param>
/// <param name="Milestone">Boolean milestone of the task</param>
/// <param name="CreatedAt">Creation date of the task.</param>
/// <param name="Start">Start date of the task</param>
/// <param name="ForecadtDate">Estimated date for completion of the task</param>
/// <param name="DeadLine">Last date for completing the task</param>
/// <param name="Complete">Actual assignment completion date</param>
/// <param name="Deliverables">A string describing the product</param>
/// <param name="Remarks">Notes on the task</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="ComplexilyLevel">The difficulty level of the task</param>
/// 
public record Task
(
    int Id,
    string? Description,
    string? Alias,
    bool Milestone,
    DateTime CreatedAt,
    DateTime Start,
    DateTime ForecastDate,
    DateTime DeadLine,
    DateTime Complete,
    string ?Deliverables=null,
    string ?Remarks=null,
    int EngineerId=0,
    EngineerExperience ComplexilyLevel=EngineerExperience.Junior
);
