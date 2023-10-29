
namespace DO;
/// <summary>
/// Engineer Entity represents a engineer with all its prop
/// </summary>
/// <param name="Id">Personal unique ID of engineer (as in national id card)</param>
/// <param name="Name">Name of the engineer</param>
/// <param name="Email">Email address of the engineer</param>
/// <param name="Level">The level of the engineer</param>
/// <param name="Cost">Hourly cost of the engineer</param>
public record Engineer
(
       int Id,
       string ?Name,
       string? Email = null,
       EngineerExperience Level = EngineerExperience.Junior,
       double? Cost = 0
);
