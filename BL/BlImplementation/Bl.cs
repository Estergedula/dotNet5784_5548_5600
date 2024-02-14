namespace BlImplementation;

using BlApi;
/// <summary>
/// A main class that implements the main logical interface IBl
/// </summary>
internal class Bl : IBl
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IMilestone Milestone => new MilestoneImplementation();
    public ITaskInList TaskInList => new TaskInListImplementation();
    public IEngineerInList EngineerInList => new EngineerInListImplementation();

}
