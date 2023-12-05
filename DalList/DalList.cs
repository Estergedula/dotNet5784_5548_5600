//using Dal;
using DalApi;
namespace Dal;
/// <summary>
/// General configuration data
/// </summary>
sealed internal  class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();
}
