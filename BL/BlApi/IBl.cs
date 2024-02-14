using DalApi;

namespace BlApi;
/// <summary>
/// A main logical interface that centralizes access to logical sub-interfaces.
/// </summary>
public interface IBl
{
    public ITask Task { get; }
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public ITaskInList TaskInList { get; }
    public IEngineerInList EngineerInList { get; }

}
