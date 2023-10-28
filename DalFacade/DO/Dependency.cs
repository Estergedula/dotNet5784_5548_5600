
namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="DependentTask"></param>
/// <param name="DependOnTask"></param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependOnTask
 );
