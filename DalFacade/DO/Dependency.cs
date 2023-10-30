
namespace DO;
/// <summary>
///  Dependency Entity represents an engineer with all its props
/// </summary>
/// <param name="Id">unique ID (created automatically) </param>
/// <param name="DependentTask">ID number of pending task</param>
/// <param name="DependOnTask">ID number of a previous assignment</param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependOnTask
 );
