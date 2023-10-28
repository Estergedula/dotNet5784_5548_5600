namespace DO;


public record Task
(
    int Id,
    string Description,
    string Alias,
    bool Milestone,
    DateTime CreatedAt,
    DateTime Start,
    DateTime ScheduledDate,
    DateTime ForecadtDate,
    DateTime Deadline,
    DateTime Complete,
    string Deliverables,
    string Remarks,
    int EngineerId,
    EngineerExperience ComplexilyLevel
);
