using Dal;
using DalApi;
using System.Runtime.CompilerServices;

namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependency? s_dalDependencys = new DependencyImplementation();
    public static void writeMenu()
    {
        Console.WriteLine("Welcome To Our Program \nTo exit type 0 \nTo Engineers type 1 \nTo Tasks type 2 \nTo Dependencies type 3 ");
    }
    public static void engineerMenu() 
    { 
        
    }
    public static void taskMenu()
    {

    }
    public static void dependencyMenu()
    {

    }
    static void Main(string[] args)
    {

        Initialization.Do(s_dalEngineer,s_dalTask,s_dalDependencys);
        writeMenu();
        int myChoice = 0;
        myChoice=Convert.ToInt32( Console.ReadLine());
        while(myChoice != 0)
        {
            switch(myChoice)
            {
                case 1:
                    engineerMenu();
                    break;
                case 2:
                    taskMenu();
                    break;
                case 3:
                    dependencyMenu();
                    break;
            }
            writeMenu();
            myChoice=Convert.ToInt32(Console.ReadLine());
        }
    }
}