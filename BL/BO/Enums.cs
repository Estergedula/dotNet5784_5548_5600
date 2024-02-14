
using DO;

namespace BO;

/// <summary>
/// Enumeration of task statuses
/// </summary>
public enum Status
{
    Unscheduled, 
    Scheduled, 
    OnTrack, 
    InJeopardy,
    All
}

/// <summary>
/// Enumeration of engineer experiences
/// </summary>
public enum EngineerExperience
{
    Expert = 1,
    Junior,
    Tyro,
    All
}
