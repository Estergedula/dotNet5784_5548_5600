using Dal;
using DalApi;
using System.Runtime.CompilerServices;

namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependency? s_dalDependencys = new DependencyImplementation();
   
    static void Main(string[] args)
    {
        Initialization.Do(s_dalEngineer,s_dalTask,s_dalDependencys);

    }
}