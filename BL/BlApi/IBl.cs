using DalApi;

namespace BlApi;
public interface IBl
{
    public ITask Task { get; }
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public ITaskInList TaskInList { get; }
    public IEngineerInList EngineerInList { get; }

}
