using Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

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
        Console.WriteLine("type name\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("type email\n");
        string? _email = Console.ReadLine();
        Console.WriteLine("type level: 0-Expert, 1-Junior, 2-Tyro\n");
        EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("type hourly cost\n");
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
            Console.WriteLine(engineer+"\n");
    }
    public static void updateEngineer()
    {
        Console.WriteLine("enter Id to delete");
        int _idToUpDate = Convert.ToInt32(Console.ReadLine());
        Engineer? engineerToUpdate = s_dalEngineer!.Read(_idToUpDate);
        if (engineerToUpdate is null)
        {
            Console.WriteLine("the id not exist");
        }
        else { 
            Console.WriteLine(engineerToUpdate);
            Console.WriteLine("type name\n");
            string? _name = Console.ReadLine();
            Console.WriteLine("type email\n");
            string? _email = Console.ReadLine();
            Console.WriteLine("type level: 0-Expert, 1-Junior, 2-Tyro\n");
            EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("type hourly cost\n");
            double? _cost = Convert.ToDouble(Console.ReadLine());
            try { s_dalEngineer!.Update(new(_idToUpDate, _name, _email, _level, _cost)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    public static void deleteEngineer()
    {
        Console.WriteLine("enter Id to delete");
        int _idToDelete = Convert.ToInt32(Console.ReadLine());
        try
        {
            s_dalEngineer!.Delete(_idToDelete);
        }catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static int engineerMenu() 
    {
        Console.WriteLine("Please enter u=your choice 1-6:\n");
        int myChoice = Convert.ToInt32(Console.ReadLine());
        switch (myChoice)
        {
            case 1:
                break;
            case 2: createEngineer();
                break;
            case 3:displayEngineer();
                break;
            case 4:  displayAllEngineers();
                break;
            case 5:updateEngineer();
                break;
            case 6: deleteEngineer();
                break;
        }
        return myChoice;
    }
    public static int getDetailsOfTask()
    {
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
    }
    public static int creatTask()
    {
        Console.WriteLine("Create a task \ntype ID number:\n");
        int _id = Convert.ToInt32(Console.ReadLine());
        getDetailsOfTask();
        try { s_dalTask!.create(new (_id, _name, _alias, _milestone,
            _createdAt, _start, _ForecastDate, _DeadLine, _Complete,
            _Deliverables, _Deliverables, _Remarks, _engineerID)}
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static int displaytask()
    {
        Console.WriteLine("enter ID to search\n");
        int _idToSearch = Convert.ToInt32(Console.ReadLine());
        Task? findTask = s_dalTask!.Read(_idToSearch);
        if (findTask is not null)
            Console.WriteLine(findTask);
        else Console.WriteLine("There is no id task");
    }
    public static void displayAllEngineers()
    {
        List<Task> allTasks = s_dalTask!.ReadAll();
        foreach (Task task in allTasks)
            Console.WriteLine(task);
    }
    public static int updateTask()
    {
        Console.WriteLine("enter Id to delete");
        int _idToUpDate = Convert.ToInt32(Console.ReadLine());
        Task? taskToUpdate = s_dalTask!.Read(_idToUpDate);
        if (taskToUpdate is null)
        {
            Console.WriteLine("The id number does not exist.");
        }
        else
        {
            getDetailsOfTask();
            try { s_dalTask!.Update(new(_idToUpDate, _name, _alias, _milestone,
            _createdAt, _start, _ForecastDate, _DeadLine, _Complete,
            _Deliverables, _Deliverables, _Remarks, _engineerID)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    public static int taskMenu()
    {
        Console.WriteLine("Please enter u=your choice 1-6:\n");
        int myChoice = Convert.ToInt32(Console.ReadLine());
        switch (myChoice)
        {
            case 1:
                break;
            case 2:
                createTask();
                break;
            case 3:
                displayTask();
                break;
            case 4:
                displayAllTasks();
                break;
            case 5:
                updateTask();
                break;
            case 6:
                deleteTask();
                break;
        }
        return myChoice;
    }
    public static void deleteDependency()
    {
        Console.WriteLine("enter Id to delete");
        int _idToDelete = Convert.ToInt32(Console.ReadLine());
        try
        {
            s_dalDependencys!.Delete(_idToDelete);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static void createDependency()
    {
        int _idOfFirstTask;
        Console.WriteLine("Create Dependency \ntype ID of first task");
        _idOfFirstTask = Convert.ToInt32(Console.ReadLine());
        while(s_dalTask!.Read(_idOfFirstTask) is null)
        {
            Console.WriteLine("type ID of first task");
            _idOfFirstTask = Convert.ToInt32(Console.ReadLine());
        }//
        int _idOfSecondTask;
        Console.WriteLine("Create Dependency \ntype ID of second task");
        _idOfSecondTask = Convert.ToInt32(Console.ReadLine());
        while (s_dalTask!.Read(_idOfSecondTask) is null)
        {
            Console.WriteLine("type ID of second task");
            _idOfSecondTask = Convert.ToInt32(Console.ReadLine());
        }

        try { s_dalDependencys!.Create(new(0, _idOfFirstTask, _idOfSecondTask));}
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static void displayDependency()
    {
        Console.WriteLine("enter Id to search");
        int _idToSearch = Convert.ToInt32(Console.ReadLine());
        Dependency? findDependency = s_dalDependencys!.Read(_idToSearch);
        if (findDependency is not null)
            Console.WriteLine(findDependency);
        else Console.WriteLine("There is no id engineer");
    }
    public static void displayAllDependencies()
    {
        List<Dependency> allDependencies = s_dalDependencys!.ReadAll();
        foreach (Dependency dependency in allDependencies)
            Console.WriteLine(dependency+"\n");
    }
    public static void updateDependency()
    {
        Console.WriteLine("enter Id to delete");
        int _idToUpDate = Convert.ToInt32(Console.ReadLine());
        Dependency? dependencyToUpdate = s_dalDependencys!.Read(_idToUpDate);
        if (dependencyToUpdate is null)
        {
            Console.WriteLine("the id not exist");
        }
        else
        {
            int _idOfFirstTask;
            Console.WriteLine(dependencyToUpdate);
            Console.WriteLine("Up Date Dependency \ntype ID of first task");
            _idOfFirstTask = Convert.ToInt32(Console.ReadLine());
            while (s_dalTask!.Read(_idOfFirstTask) is null)
            {
                Console.WriteLine("type ID of first task");
                _idOfFirstTask = Convert.ToInt32(Console.ReadLine());
            }
            int _idOfSecondTask;
            Console.WriteLine("Create Dependency \ntype ID of second task");
            _idOfSecondTask = Convert.ToInt32(Console.ReadLine());
            while (s_dalTask!.Read(_idOfSecondTask) is null)
            {
                Console.WriteLine("type ID of second task");
                _idOfSecondTask = Convert.ToInt32(Console.ReadLine());
            }
            try { s_dalDependencys!.Update(new(_idToUpDate,_idOfFirstTask,_idOfSecondTask)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    public static int dependencyMenu()
    {
        Console.WriteLine();
        int myChoice = Convert.ToInt32(Console.ReadLine());
        switch (myChoice)
        {
            case 1:
                break;
            case 2:
                createDependency();
                break;
            case 3:
                displayDependency();
                break;
            case 4:
                displayAllDependencies();
                break;
            case 5:
                updateDependency();
                break;
            case 6:
                deleteDependency();
                break;
        }
        return myChoice;
    }
    static void Main(string[] args)
    {
        Initialization.Do(s_dalEngineer,s_dalTask,s_dalDependencys);
        writeMenu();
        int myChoice = 0;
        myChoice=Convert.ToInt32( Console.ReadLine());
        while(myChoice != 0)
        {
            int innerChoice = 0;
            switch(myChoice)
            {
                case 1:
                    innerChoice= engineerMenu();
                    break;
                case 2:
                    innerChoice= taskMenu();
                    break;
                case 3:
                    innerChoice= dependencyMenu();
                    break;
            }
            if (innerChoice==1)
                break;
            writeMenu();
            myChoice=Convert.ToInt32(Console.ReadLine());
        }
    }
}