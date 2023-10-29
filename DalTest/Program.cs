using Dal;
using DalApi;
using DO;
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
    public static void createEngineer() 
    {
        Console.WriteLine("Create Engineer \ntype ID");
        int _id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("type name");
        string? _name = Console.ReadLine();
        Console.WriteLine("type email");
        string? _email = Console.ReadLine();
        Console.WriteLine("type level: 0-Expert, 1-Junior, 2-Tyro");
        EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("type hourly cost");
        double? _cost = Convert.ToDouble(Console.ReadLine());
        try { s_dalEngineer!.Create(new(_id, _name, _email, _level, _cost)); }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static void displayEngineer()
    {
        Console.WriteLine("enter Id to search");
        int _idToSearch = Convert.ToInt32(Console.ReadLine());
        Engineer? findEngineer = s_dalEngineer!.Read(_idToSearch);
        if (findEngineer is not null)
            Console.WriteLine(findEngineer);
        else Console.WriteLine("There is no id engineer");
    }
    public static void displayAllEngineers()
    {
        List<Engineer> allEngineers = s_dalEngineer!.ReadAll();
        foreach(Engineer engineer in allEngineers)
            Console.WriteLine(engineer);
    }
    public static void updateEngineer()
    {

    }
    public static int engineerMenu() 
    {
        Console.WriteLine();
        int myChoice = Convert.ToInt32(Console.ReadLine());
        switch (myChoice)
        {
            case 1: break;
            case 2: createEngineer();
                break;
            case 3:displayEngineer();
                break;
            case 4:
                displayAllEngineers();
                break;
            case 5:
               updateEngineer();//
                break;
        }
        return myChoice;
    }
    public static int creatTask()
    {
        Console.WriteLine("Create a task \ntype ID number:\n");
        int _id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("type description:\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("type alias:\n");
        string? _alias = Console.ReadLine();
        Console.WriteLine("type milestone:\n");
        bool _milestone = Convert.ToBoolean(Console.ReadLine());
        Console.WriteLine("type date created\n");
        DateTime _createdAt = Convert.ToDateTime(Console.ReadLine())
        Console.WriteLine("type date started\n");
        DateTime _start = Convert.ToDateTime(Console.ReadLine())
        Console.WriteLine("type date of forecast\n");
        DateTime _ForecastDate = Convert.ToDateTime(Console.ReadLine())
        Console.WriteLine("type date of deadline\n");
        DateTime _DeadLine = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("type date of complete\n");
        DateTime _Complete = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("type deliverables\n");
        string? _Deliverables = Console.ReadLine();
        Console.WriteLine("type remarks\n");
        string? _Remarks = Console.ReadLine();
        Console.WriteLine("type ID of engineer\n");
        int _engineerID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("type engineer experience")
    }
    public static int taskMenu()
    {
        return 0;
    }
    public static int dependencyMenu()
    {
        return 0;
    }
    static void Main(string[] args)
    {

        Initialization.Do(s_dalEngineer,s_dalTask,s_dalDependencys);
        writeMenu();
        int myChoice = 0;
        myChoice=Convert.ToInt32( Console.ReadLine());
        while(myChoice != 0)
        {
            int innerSwitch = 0;
            switch(myChoice)
            {
                case 1:
                    innerSwitch= engineerMenu();
                    break;
                case 2:
                    innerSwitch= taskMenu();
                    break;
                case 3:
                    innerSwitch= dependencyMenu();
                    break;
            }
            if (innerSwitch==1)
                break;
            writeMenu();
            myChoice=Convert.ToInt32(Console.ReadLine());
        }
    }
}