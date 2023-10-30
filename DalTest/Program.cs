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
    public static int writeMenu()//Explain for the user the options of the main menu and input his choice
    {
        Console.WriteLine("Welcome To Our Program \nTo exit type 0 \nTo Engineers type 1 \nTo Tasks type 2 \nTo Dependencies type 3 ");
       int  myChoice = Convert.ToInt32(Console.ReadLine());
        return myChoice;
    }
    public static int writeInnerMenue()//Explain for the user the options of the inner menu of each entity and input his choice
    {
        Console.WriteLine("Please enter your choice \nType 1 to exit \nType 2 to create a new \nType 3 to display \nType 4 to display all \nType 5 to update \nType 6 to delate");
        int myChoice = Convert.ToInt32(Console.ReadLine());
        return myChoice;
    }   

    public static void createEngineer() //input details of new engineer and create
    {
        Console.WriteLine("Create Engineer \ntype ID");
        int _id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter name\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("enter email\n");
        string? _email = Console.ReadLine();
        Console.WriteLine("enter level: 0-Expert, 1-Junior, 2-Tyro\n");
        EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter hourly cost\n");
        double? _cost = Convert.ToDouble(Console.ReadLine());
        try { int id =  s_dalEngineer!.Create(new(_id, _name, _email, _level, _cost));
            Console.WriteLine(id + "\n");
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static void displayEngineer()//input id of engineer and display his details
    {
        Console.WriteLine("enter Id to search");
        int _idToSearch = Convert.ToInt32(Console.ReadLine());
        Engineer? findEngineer = s_dalEngineer!.Read(_idToSearch);
        if (findEngineer is not null)
            Console.WriteLine(findEngineer);
        else Console.WriteLine("There is no id engineer");
    }
    public static void displayAllEngineers()//diplay all engineers
    {
        List<Engineer> allEngineers = s_dalEngineer!.ReadAll();
        foreach(Engineer engineer in allEngineers)
            Console.WriteLine(engineer+"\n");
    }
    public static void updateEngineer()//input id of engineer, his details and update
    {
        Console.WriteLine("Enter Id to delete");
        int _idToUpDate = Convert.ToInt32(Console.ReadLine());
        Engineer? engineerToUpdate = s_dalEngineer!.Read(_idToUpDate);
        if (engineerToUpdate is null)
        {
            Console.WriteLine("This id number does not exist");
        }
        else { 
            Console.WriteLine(engineerToUpdate);
            Console.WriteLine("Enter name\n");
            string? _name = Console.ReadLine();
            Console.WriteLine("Enter email\n");
            string? _email = Console.ReadLine();
            Console.WriteLine("Enter level: 0-Expert, 1-Junior, 2-Tyro\n");
            EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter hourly cost\n");
            double? _cost = Convert.ToDouble(Console.ReadLine());
            try { s_dalEngineer!.Update(new(_idToUpDate, _name, _email, _level, _cost)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    public static void deleteEngineer()
    {
        Console.WriteLine("Enter Id to delete");
        int _idToDelete = Convert.ToInt32(Console.ReadLine());
        try
        {
            s_dalEngineer!.Delete(_idToDelete);
        }catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static int engineerMenu() 
    {
        int myChoice = writeInnerMenue();
        switch (myChoice)
        {
            case 1:
                break;
            case 2: createEngineer();
                break;
            case 3:displayEngineer();
                break;
            case 4:
                displayAllEngineers();
                break;
            case 5:updateEngineer();
                break;
            case 6: deleteEngineer();
                break;
        }
        return myChoice;
    }
    public static void createTask()//input details of new task and create
    {
        Console.WriteLine("Create a task \n");
        Console.WriteLine("Enter description:\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("Enter alias:\n");
        string? _alias = Console.ReadLine();
        Console.WriteLine("Enter milestone:\n");
        bool _milestone = Convert.ToBoolean(Console.ReadLine());
        Console.WriteLine("Enter date created\n");
        DateTime _createdAt = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("Enter date started\n");
        DateTime _start = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("Enter date of forecast\n");
        DateTime _ForecastDate = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("Enter date of deadline\n");
        DateTime _DeadLine = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("Enter date of complete\n");
        DateTime _Complete = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("Enter deliverables\n");
        string? _Deliverables = Console.ReadLine();
        Console.WriteLine("Enter remarks\n");
        string? _Remarks = Console.ReadLine();
        Console.WriteLine("Enter level: 0-Expert, 1-Junior, 2-Tyro\n");
        EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter ID of engineer\n");
        int _engineerID = Convert.ToInt32(Console.ReadLine());
        while (s_dalEngineer!.Read(_engineerID) is null)
        {
            Console.WriteLine("Enter ID of first task");
            _engineerID = Convert.ToInt32(Console.ReadLine());
        }
        try {
            int id = s_dalTask!.Create(new(0, _name, _alias, _milestone,
            _createdAt, _start, _ForecastDate, _DeadLine, _Complete,
            _Deliverables, _Deliverables,_engineerID, _level));
            Console.WriteLine(id + "\n");
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static void displayTask()//input id of task and display his details
    {
        Console.WriteLine("enter ID to search\n");
        int _idToSearch = Convert.ToInt32(Console.ReadLine());
        DO.Task? findTask = s_dalTask!.Read(_idToSearch);
        if (findTask is not null)
            Console.WriteLine(findTask);
        else Console.WriteLine("There is no id task");
    }
    public static void displayAllTasks()//diplay all tasks
    {
        List<DO.Task> allTasks = s_dalTask!.ReadAll();
        foreach ( DO.Task task in allTasks)
            Console.WriteLine(task);
    }
    public static void updateTask()//input id of task, his details and update
    {
        Console.WriteLine("Enter Id to delete");
        int _idToUpDate = Convert.ToInt32(Console.ReadLine());
        DO.Task? taskToUpdate = s_dalTask!.Read(_idToUpDate);
        if (taskToUpdate is null)
        {
            Console.WriteLine("The id number does not exist.");
        }
        else
        {
            Console.WriteLine("Update a task \n ");
            Console.WriteLine("Enter description:\n");
            string? _name = Console.ReadLine();
            Console.WriteLine("Enter alias:\n");
            string? _alias = Console.ReadLine();
            Console.WriteLine("Enter milestone:\n");
            bool _milestone = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Enter date created\n");
            DateTime _createdAt = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter date started\n");
            DateTime _start = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter date of forecast\n");
            DateTime _ForecastDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter date of deadline\n");
            DateTime _DeadLine = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter date of complete\n");
            DateTime _Complete = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter deliverables\n");
            string? _Deliverables = Console.ReadLine();
            Console.WriteLine("Enter remarks\n");
            string? _Remarks = Console.ReadLine();
            Console.WriteLine("Enter level: 0-Expert, 1-Junior, 2-Tyro\n");
            EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter ID of engineer\n");
            int _engineerID = Convert.ToInt32(Console.ReadLine());
            while (s_dalEngineer!.Read(_engineerID) is null)
            {
                Console.WriteLine("Enter ID of first task");
                _engineerID = Convert.ToInt32(Console.ReadLine());
            }
            try { s_dalTask!.Update(new(0, _name, _alias, _milestone, _createdAt, _start, _ForecastDate, _DeadLine, _Complete, _Deliverables, _Remarks, _engineerID, _level)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    public static void deleteTask()
    {
        Console.WriteLine("Enter Id to delete");
        int _idToDelete = Convert.ToInt32(Console.ReadLine());
        try
        {
            s_dalTask!.Delete(_idToDelete);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static int taskMenu()
    {
        int myChoice = writeInnerMenue();
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
        Console.WriteLine("Enter Id to delete");
        int _idToDelete = Convert.ToInt32(Console.ReadLine());
        try
        {
            s_dalDependencys!.Delete(_idToDelete);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static void createDependency()//input details of new dependency and create
    {
        int _idOfFirstTask;
        Console.WriteLine("Create Dependency \ntype ID of first task");
        _idOfFirstTask = Convert.ToInt32(Console.ReadLine());
        while(s_dalTask!.Read(_idOfFirstTask) is null)
        {
            Console.WriteLine("Enter ID of first task");
            _idOfFirstTask = Convert.ToInt32(Console.ReadLine());
        }//
        int _idOfSecondTask;
        Console.WriteLine("Create Dependency \ntype ID of second task");
        _idOfSecondTask = Convert.ToInt32(Console.ReadLine());
        while (s_dalTask!.Read(_idOfSecondTask) is null)
        {
            Console.WriteLine("Enter ID of second task");
            _idOfSecondTask = Convert.ToInt32(Console.ReadLine());
        }

        try { int id = s_dalDependencys!.Create(new(0, _idOfFirstTask, _idOfSecondTask));
            Console.WriteLine(id+"\n");
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public static void displayDependency()//input id of Dependency and display his details
    {
        Console.WriteLine("Enter Id to search");
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
    public static void updateDependency()//input id of task//input id of engineer, his details and update, his details and update
    {
        Console.WriteLine("Enter Id to delete");
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
                Console.WriteLine("Enter ID of second task");
                _idOfSecondTask = Convert.ToInt32(Console.ReadLine());
            }
            try { s_dalDependencys!.Update(new(_idToUpDate,_idOfFirstTask,_idOfSecondTask)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    public static int dependencyMenu()
    {
        int myChoice = writeInnerMenue();
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
        int myChoice=  writeMenu();
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