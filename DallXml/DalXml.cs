using DalApi;
namespace Dal;
sealed public class DalXml : IDal
{
    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();
}
