using Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace DalTest;
internal class Program
{
    static readonly IDal s_dal = new DalList();
    /// <summary>
    /// Explain for the user the options of the main menu and input his choice
    /// </summary>
    /// <returns>value of the user choice</returns>
    public static int writeMenu()
    {
        Console.WriteLine("Welcome To Our Program \nTo exit type 0 \nTo Engineers type 1 \nTo Tasks type 2 \nTo Dependencies type 3 ");
        int myChoice;
        int.TryParse(Console.ReadLine(), out myChoice);   
        return myChoice;
    }
    /// <summary>
    /// Explain for the user the options of the inner menu of each entity and input his choice
    /// </summary>
    /// <returns>value of the user choice</returns>
    public static int writeInnerMenue()
    {
       Console.WriteLine("Please enter your choice \nType 1 to exit \nType 2 to create a new \nType 3 to display \nType 4 to display all \nType 5 to update \nType 6 to delate");
       int myChoice;
       int.TryParse(Console.ReadLine(),out myChoice);
        return myChoice;
    }
    /// <summary>
    /// input details of new engineer and create
    /// </summary>
    public static void createEngineer() 
    {
        Console.WriteLine("Create Engineer \ntype ID");
        int _id;
        int.TryParse(Console.ReadLine(), out _id);
        Console.WriteLine("enter name\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("enter email\n");
        string? _email = Console.ReadLine();
        Console.WriteLine("enter level: 0-Expert, 1-Junior, 2-Tyro\n");
        int choiceExperience;
        int.TryParse(Console.ReadLine(), out choiceExperience);
        EngineerExperience _level = (EngineerExperience)choiceExperience;
        Console.WriteLine("enter hourly cost\n");
        double _cost;
        double.TryParse(Console.ReadLine(),out _cost);
        try { int id =  s_dal!.Engineer.Create(new(_id, _name, _email, _level, _cost));
            Console.WriteLine(id + "\n");
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    /// <summary>
    /// input id of engineer and display his details
    /// </summary>
    public static void displayEngineer()
    {
        Console.WriteLine("enter Id to search");
        int _idToSearch;
        int.TryParse(Console.ReadLine(),out _idToSearch);
        Engineer? findEngineer = s_dal!.Engineer.Read(_idToSearch);
        if (findEngineer is not null)
            Console.WriteLine(findEngineer);
        else Console.WriteLine("There is no id engineer");
    }
    /// <summary>
    /// diplay all engineers
    /// </summary>
    public static void displayAllEngineers()
    {
        List<Engineer> allEngineers = s_dal!.Engineer.ReadAll();
        foreach(Engineer engineer in allEngineers)
            Console.WriteLine(engineer+"\n");
    }
    /// <summary>
    /// input id of engineer, his details and update
    /// </summary>
    public static void updateEngineer()
    {
        Console.WriteLine("Enter Id to delete");
        int _idToUpDate;
        int.TryParse(Console.ReadLine(),out _idToUpDate);
        Engineer? engineerToUpdate = s_dal!.Engineer.Read(_idToUpDate);
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
            double _cost;
             double.TryParse(Console.ReadLine(),out _cost);
            try { s_dal!.Engineer.Update(new(_idToUpDate, _name, _email, _level, _cost)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    /// <summary>
    /// input id of engineer and delete
    /// </summary>
    public static void deleteEngineer()
    {
        Console.WriteLine("Enter Id to delete");
        int _idToDelete;
        int.TryParse(Console.ReadLine(),out _idToDelete);
        try
        {
            s_dal!.Engineer.Delete(_idToDelete);
        }catch (Exception e) { Console.WriteLine(e.Message); }
    }
    /// <summary>
    /// display the option of engineer menu and do the user choice
    /// </summary>
    /// <returns>the user choice</returns>
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
    /// <summary>
    /// input details of new task and create
    /// </summary>
    public static void createTask()
    {
        Console.WriteLine("Create a task \n");
        Console.WriteLine("Enter description:\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("Enter alias:\n");
        string? _alias = Console.ReadLine();
        Console.WriteLine("Enter milestone:\n");
        bool _milestone;
        bool.TryParse(Console.ReadLine(),out _milestone);
        Console.WriteLine("Enter date created\n");
        DateTime _createdAt;
        DateTime.TryParse(Console.ReadLine(),out _createdAt);
        Console.WriteLine("Enter date started\n");
        DateTime _start;
        DateTime.TryParse(Console.ReadLine(),out _start);
        Console.WriteLine("Enter date of forecast\n");
        DateTime _ForecastDate;
        DateTime.TryParse(Console.ReadLine(),out _ForecastDate);
        Console.WriteLine("Enter date of deadline\n");
        DateTime _DeadLine;
        DateTime.TryParse(Console.ReadLine(),out _DeadLine);
        Console.WriteLine("Enter date of complete\n");
        DateTime _Complete;
        DateTime.TryParse(Console.ReadLine(),out _Complete);
        Console.WriteLine("Enter deliverables\n");
        string? _Deliverables = Console.ReadLine();
        Console.WriteLine("Enter remarks\n");
        string? _Remarks = Console.ReadLine();
        Console.WriteLine("Enter level: 0-Expert, 1-Junior, 2-Tyro\n");
        int experienceChoice;
        int.TryParse(Console.ReadLine(), out experienceChoice);
        EngineerExperience _level = (EngineerExperience)experienceChoice;
        Console.WriteLine("Enter ID of engineer\n");
        int _engineerID;
        int.TryParse(Console.ReadLine(),out _engineerID);
        while (s_dal!.Engineer.Read(_engineerID) is null)
        {
            Console.WriteLine("Enter ID of first task");
            int.TryParse(Console.ReadLine(), out _engineerID);
        }
        try {
            int id = s_dal!.Task.Create(new(0, _name, _alias, _milestone,
            _createdAt, _start, _ForecastDate, _DeadLine, _Complete,
            _Deliverables, _Deliverables,_engineerID, _level));
            Console.WriteLine(id + "\n");
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    /// <summary>
    /// input id of task and display his details
    /// </summary>
    public static void displayTask()
    {
        Console.WriteLine("enter ID to search\n");
        int _idToSearch;
        int.TryParse(Console.ReadLine(),out _idToSearch);
        DO.Task? findTask = s_dal!.Task.Read(_idToSearch);
        if (findTask is not null)
            Console.WriteLine(findTask);
        else Console.WriteLine("There is no id task");
    }
    /// <summary>
    /// diplay all tasks
    /// </summary>
    public static void displayAllTasks()
    {
        List<DO.Task> allTasks = s_dal!.Task.ReadAll();
        foreach ( DO.Task task in allTasks)
            Console.WriteLine(task);
    }
    /// <summary>
    /// input id of task, his details and update
    /// </summary>
    public static void updateTask()
    {
        Console.WriteLine("Enter Id to delete");
        int _idToUpDate;
        int.TryParse(Console.ReadLine(),out _idToUpDate);
        DO.Task? taskToUpdate = s_dal!.Task.Read(_idToUpDate);
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
            bool _milestone;
            bool.TryParse(Console.ReadLine(),out _milestone);
            Console.WriteLine("Enter date created\n");
            DateTime _createdAt;
            DateTime.TryParse(Console.ReadLine(),out _createdAt);
            Console.WriteLine("Enter date started\n");
            DateTime _start;
            DateTime.TryParse(Console.ReadLine(),out _start);
            Console.WriteLine("Enter date of forecast\n");
            DateTime _ForecastDate;
            DateTime.TryParse(Console.ReadLine(),out _ForecastDate);
            Console.WriteLine("Enter date of deadline\n");
            DateTime _DeadLine;
            DateTime.TryParse(Console.ReadLine(),out _DeadLine);
            Console.WriteLine("Enter date of complete\n");
            DateTime _Complete;
            DateTime.TryParse(Console.ReadLine(),out _Complete);
            Console.WriteLine("Enter deliverables\n");
            string? _Deliverables = Console.ReadLine();
            Console.WriteLine("Enter remarks\n");
            string? _Remarks = Console.ReadLine();
            Console.WriteLine("Enter level: 0-Expert, 1-Junior, 2-Tyro\n");
            int experienceChoice;
            int.TryParse(Console.ReadLine(), out experienceChoice);
            EngineerExperience _level = (EngineerExperience)experienceChoice;
            Console.WriteLine("Enter ID of engineer\n");
            int _engineerID;
            int.TryParse(Console.ReadLine(),out _engineerID);
            while (s_dal!.Engineer .Read(_engineerID) is null)
            {
                Console.WriteLine("Enter ID of first task");
                int.TryParse(Console.ReadLine(), out _engineerID);
            }
            try { s_dal!.Task.Update(new(0, _name, _alias, _milestone, _createdAt, _start, _ForecastDate, _DeadLine, _Complete, _Deliverables, _Remarks, _engineerID, _level)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    /// <summary>
    /// input id of task and delete
    /// </summary>
    public static void deleteTask()
    {
        Console.WriteLine("Enter Id to delete");
        int _idToDelete;
        int.TryParse(Console.ReadLine(),out _idToDelete);
        try
        {
            s_dal!.Task.Delete(_idToDelete);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    /// <summary>
    /// display the option of engineer menu and do the user choice
    /// </summary>
    /// <returns>the user choice</returns>
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
    /// <summary>
    /// input id of dependency and delete
    /// </summary>
    public static void deleteDependency()
    {
        Console.WriteLine("Enter Id to delete");
        int _idToDelete;
        int.TryParse(Console.ReadLine(),out _idToDelete);
        try
        {
            s_dal.Dependency!.Delete(_idToDelete);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    /// <summary>
    /// input details of new dependency and create
    /// </summary>
    public static void createDependency()
    {
        int _idOfFirstTask;
        Console.WriteLine("Create Dependency \ntype ID of first task");
        int.TryParse(Console.ReadLine(),out _idOfFirstTask);
        while(s_dal!.Dependency.Read(_idOfFirstTask) is null)
        {
            Console.WriteLine("Enter ID of first task");
            int.TryParse(Console.ReadLine(), out _idOfFirstTask);
        }//
        int _idOfSecondTask;
        Console.WriteLine("Create Dependency \ntype ID of second task");
        int.TryParse(Console.ReadLine(), out _idOfSecondTask);
        while (s_dal!.Task.Read(_idOfSecondTask) is null)
        {
            Console.WriteLine("Enter ID of second task");
            int.TryParse(Console.ReadLine(), out _idOfSecondTask);
        }

        try { int id = s_dal!.Dependency.Create(new(0, _idOfFirstTask, _idOfSecondTask));
            Console.WriteLine(id+"\n");
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    /// <summary>
    /// input id of Dependency and display his details
    /// </summary>
    public static void displayDependency()
    {
        Console.WriteLine("Enter Id to search");
        int _idToSearch;
        int.TryParse(Console.ReadLine(),out _idToSearch);
        Dependency? findDependency = s_dal!.Dependency.Read(_idToSearch);
        if (findDependency is not null)
            Console.WriteLine(findDependency);
        else Console.WriteLine("There is no id engineer");
    }
    /// <summary>
    /// diplay all dependencies
    /// </summary>
    public static void displayAllDependencies()
    {
        List<Dependency> allDependencies = s_dal.Dependency!.ReadAll();
        foreach (Dependency dependency in allDependencies)
            Console.WriteLine(dependency+"\n");
    }
    /// <summary>
    /// input id of Dependency, his details and update, his details and update
    /// </summary>
    public static void updateDependency()
    { 
        Console.WriteLine("Enter Id to delete");
        int _idToUpDate;
        int.TryParse(Console.ReadLine(),out _idToUpDate);
        Dependency? dependencyToUpdate = s_dal!.Dependency.Read(_idToUpDate);
        if (dependencyToUpdate is null)
        {
            Console.WriteLine("the id not exist");
        }
        else
        {
            int _idOfFirstTask;
            Console.WriteLine(dependencyToUpdate);
            Console.WriteLine("Up Date Dependency \ntype ID of first task");
            int.TryParse(Console.ReadLine(),out _idOfFirstTask);
            while (s_dal!.Task.Read(_idOfFirstTask) is null)
            {
                Console.WriteLine("type ID of first task");
                int.TryParse(Console.ReadLine(), out _idOfFirstTask);
            }
            int _idOfSecondTask;
            Console.WriteLine("Create Dependency \ntype ID of second task");
            int.TryParse(Console.ReadLine(), out _idOfSecondTask);
            while (s_dal!.Task.Read(_idOfSecondTask) is null)
            {
                Console.WriteLine("Enter ID of second task");
                int.TryParse(Console.ReadLine(), out _idOfSecondTask);
            }
            try { s_dal!.Dependency.Update(new(_idToUpDate,_idOfFirstTask,_idOfSecondTask)); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    /// <summary>
    /// display the option of dependency menu and do the user choice
    /// </summary>
    /// <returns>the user choice</returns>
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
    /// <summary>
    /// main method of the program
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Initialization.Do(s_dal);
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
            int.TryParse(Console.ReadLine(), out myChoice);
        }
    }
}