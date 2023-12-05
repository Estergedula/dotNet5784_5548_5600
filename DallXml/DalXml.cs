using DalApi;

namespace Dal;
/// <summary>
/// A class that inherits and implements the interface by initializing the subinterfaces in the access classes
/// </summary>
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; }  = new DalXml();
    private DalXml() { }
    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();
}
