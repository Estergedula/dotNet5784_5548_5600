using Dal;
using DalApi;
using DO;

namespace DalTest;
internal class Program
{
    //static readonly IDal s_dal = new DalXml();
    static readonly IDal s_dal = Factory.Get;
    /// <summary>
    /// Explain for the user the options of the main menu and input his choice
    /// </summary>
    /// <returns>value of the user choice</returns>
    public static int WriteMenu()
    {
        Console.WriteLine("Welcome To Our Program \nTo exit type 0 \nTo Engineers type 1 \nTo Tasks type 2 \nTo Dependencies type 3 ");
        int.TryParse(Console.ReadLine(), out int myChoice);
        return myChoice;
    }
    /// <summary>
    /// Explain for the user the options of the inner menu of each entity and input his choice
    /// </summary>
    /// <returns>value of the user choice</returns>
    public static int WriteInnerMenue()
    {
        Console.WriteLine("Please enter your choice \nType 1 to exit \nType 2 to create a new \nType 3 to display \nType 4 to display all \nType 5 to update \nType 6 to delate");
        int.TryParse(Console.ReadLine(), out int myChoice);
        return myChoice;
    }
    /// <summary>
    /// input details of new engineer and create
    /// </summary>
    public static void CreateEngineer()
    {
        Console.WriteLine("Create Engineer \ntype ID");
        int.TryParse(Console.ReadLine(), out int _id);
        Console.WriteLine("enter name\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("enter email\n");
        string? _email = Console.ReadLine();
        Console.WriteLine("enter level: 1-Expert, 2-Junior, 3-Tyro\n");
        int.TryParse(Console.ReadLine(), out int choiceExperience);
        EngineerExperience _level = (EngineerExperience)choiceExperience;
        Console.WriteLine("enter hourly cost\n");
        double.TryParse(Console.ReadLine(), out double _cost);
        try
        {
            int id = s_dal!.Engineer.Create(new(_id, _name, _email, _level, _cost));
            Console.WriteLine(id + "\n");
        }
        catch (DalAlreadyExistsException e) { Console.WriteLine(e.Message + "\n"); }
    }
    /// <summary>
    /// input id of engineer and display his details
    /// </summary>
    public static void DisplayEngineer()
    {
        Console.WriteLine("enter Id to search");
        int.TryParse(Console.ReadLine(), out int _idToSearch);
        Engineer? findEngineer = s_dal!.Engineer.Read(_idToSearch);
        if (findEngineer is not null)
            Console.WriteLine(findEngineer);
        else Console.WriteLine("There is no id engineer");
    }
    /// <summary>
    /// diplay all engineers
    /// </summary>
    public static void DisplayAllEngineers()
    {
        List<Engineer?> allEngineers = s_dal!.Engineer.ReadAll().ToList();
        foreach (Engineer? engineer in allEngineers)
            Console.WriteLine(engineer + "\n");
    }
    /// <summary>
    /// input id of engineer, his details and update
    /// </summary>
    public static void UpdateEngineer()
    {
        Console.WriteLine("Enter Id to update");
        int.TryParse(Console.ReadLine(), out int _idToUpDate);
        Engineer? engineerToUpdate = s_dal!.Engineer.Read(_idToUpDate);
        if (engineerToUpdate is null)
        {
            Console.WriteLine("This id number does not exist");
        }
        else
        {
            Console.WriteLine(engineerToUpdate);
            Console.WriteLine("Enter name\n");
            string? _name = Console.ReadLine();
            Console.WriteLine("Enter email\n");
            string? _email = Console.ReadLine();
            Console.WriteLine("Enter level: 1-Expert, 2-Junior, 3-Tyro\n");
            int.TryParse(Console.ReadLine(), out int experienceChoice);
            if (experienceChoice==0)
                experienceChoice=(int)engineerToUpdate.Level;
            EngineerExperience _level = (EngineerExperience)experienceChoice;
            Console.WriteLine("Enter hourly cost\n");
            double.TryParse(Console.ReadLine(), out double _cost);
            if (_cost==0)
            {
                if(engineerToUpdate.Cost is not null)
                    _cost = (double)engineerToUpdate.Cost;
            }
            try { s_dal!.Engineer.Update(new(_idToUpDate, _name??engineerToUpdate.Name, _email??engineerToUpdate.Email, _level, _cost)); }
            catch (DalDoesNotExistException e) { Console.WriteLine(e.Message + "\n"); }
        }
    }
    /// <summary>
    /// input id of engineer and delete
    /// </summary>
    public static void DeleteEngineer()
    {
        Console.WriteLine("Enter Id to delete");
        int.TryParse(Console.ReadLine(), out int _idToDelete);
        try
        {
            s_dal!.Engineer.Delete(_idToDelete);
        }
        catch (DalDoesNotExistException e) { Console.WriteLine(e.Message + "\n"); }
    }
    /// <summary>
    /// display the option of engineer menu and do the user choice
    /// </summary>
    /// <returns>the user choice</returns>
    public static int EngineerMenu()
    {
        int myChoice = WriteInnerMenue();
        switch (myChoice)
        {
            case 1:
                break;
            case 2:
                CreateEngineer();
                break;
            case 3:
                DisplayEngineer();
                break;
            case 4:
                DisplayAllEngineers();
                break;
            case 5:
                UpdateEngineer();
                break;
            case 6:
                DeleteEngineer();
                break;
        }
        return myChoice;
    }
    /// <summary>
    /// input details of new task and create
    /// </summary>
    public static void CreateTask()
    {
        Console.WriteLine("Create a task \n");
        Console.WriteLine("Enter description:\n");
        string? _name = Console.ReadLine();
        Console.WriteLine("Enter alias:\n");
        string? _alias = Console.ReadLine();
        Console.WriteLine("Enter milestone:\n");
        bool.TryParse(Console.ReadLine(), out bool _milestone);
        DateTime _createdAt=DateTime.Now;
        DateTime _start=DateTime.MinValue;
        Console.WriteLine("Enter date of forecast\n");
        DateTime _ForecastDate;
        DateTime.TryParse(Console.ReadLine(), out _ForecastDate);
        Console.WriteLine("Enter date of deadline\n");
        DateTime.TryParse(Console.ReadLine(), out DateTime _DeadLine);
        DateTime _Complete=DateTime.MinValue;
        Console.WriteLine("Enter deliverables\n");
        string? _Deliverables = Console.ReadLine();
        Console.WriteLine("Enter remarks\n");
        string? _Remarks = Console.ReadLine();
        Console.WriteLine("Enter level: 1-Expert, 2-Junior, 3-Tyro\n");
        int experienceChoice;
        int.TryParse(Console.ReadLine(), out experienceChoice);
        EngineerExperience _level = (EngineerExperience)experienceChoice;
        Console.WriteLine("Enter ID of engineer\n");
        int.TryParse(Console.ReadLine(), out int _engineerID);
        while (s_dal!.Engineer.Read(_engineerID) is null)
        {
            Console.WriteLine("Enter ID of engineer\n");
            int.TryParse(Console.ReadLine(), out _engineerID);
        }
        int id = s_dal!.Task.Create(new(0, _name, _alias, _milestone,
        _createdAt, _start, _ForecastDate, _DeadLine, _Complete,
        _Deliverables, _Deliverables, _engineerID, _level));
        Console.WriteLine(id + "\n");

    }
    /// <summary>
    /// input id of task and display his details
    /// </summary>
    public static void DisplayTask()
    {
        Console.WriteLine("enter ID to search\n");
        int.TryParse(Console.ReadLine(), out int _idToSearch);
        DO.Task? findTask = s_dal!.Task.Read(_idToSearch);
        if (findTask is not null)
            Console.WriteLine(findTask);
        else Console.WriteLine("There is no id task");
    }
    /// <summary>
    /// diplay all tasks
    /// </summary>
    public static void DisplayAllTasks()
    {
        List<DO.Task?> allTasks = s_dal!.Task.ReadAll().ToList();
        foreach (DO.Task? task in allTasks)
            Console.WriteLine(task);
    }
    /// <summary>
    /// input id of task, his details and update
    /// </summary>
    public static void UpdateTask()
    {
        Console.WriteLine("Enter Id to update");
        int.TryParse(Console.ReadLine(), out int _idToUpDate);
        DO.Task? taskToUpdate = s_dal!.Task.Read(_idToUpDate);
        if (taskToUpdate is null)
        {
            Console.WriteLine("The id number does not exist.");
        }
        else
        {
            Console.WriteLine("Update a task \n ");
            Console.WriteLine("Enter description:\n");
            string? _description = Console.ReadLine();
            Console.WriteLine("Enter alias:\n");
            string? _alias = Console.ReadLine();
            Console.WriteLine("Enter milestone:\n");
            bool.TryParse(Console.ReadLine(), out bool _milestone);
            Console.WriteLine("Enter date created\n");
            DateTime _createdAt= DateTime.Now;
            Console.WriteLine("Enter date started\n");
            DateTime _start;
            DateTime.TryParse(Console.ReadLine(), out  _start);
            Console.WriteLine("Enter date of forecast\n");
            DateTime.TryParse(Console.ReadLine(), out DateTime _ForecastDate);
            Console.WriteLine("Enter date of deadline\n");
            DateTime.TryParse(Console.ReadLine(), out DateTime _DeadLine);
            Console.WriteLine("Enter date of complete\n");
            DateTime.TryParse(Console.ReadLine(), out DateTime _Complete);
            Console.WriteLine("Enter deliverables\n");
            string? _Deliverables = Console.ReadLine();
            Console.WriteLine("Enter remarks\n");
            string? _Remarks = Console.ReadLine();
            Console.WriteLine("Enter level: 1-Expert, 2-Junior, 3-Tyro\n");
            int.TryParse(Console.ReadLine(), out int experienceChoice);
            if (experienceChoice==0)
                experienceChoice=(int)taskToUpdate.ComplexilyLevel;
            EngineerExperience _level = (EngineerExperience)experienceChoice;
            Console.WriteLine("Enter ID of engineer\n");
            int.TryParse(Console.ReadLine(), out int _engineerID);
            if(_engineerID==0)
                _engineerID=taskToUpdate.EngineerId;
            else
                while (s_dal!.Engineer.Read(_engineerID) is null)
                {
                    Console.WriteLine("Enter ID of first task");
                    int.TryParse(Console.ReadLine(), out _engineerID);
                }
            try { s_dal!.Task.Update(new(taskToUpdate.Id, _description??taskToUpdate.Description, _alias??taskToUpdate.Alias, _milestone, _createdAt, _start==DateTime.MinValue?taskToUpdate.Start:_start,_ForecastDate, _DeadLine, _Complete, _Deliverables??taskToUpdate.Deliverables, _Remarks??taskToUpdate.Remarks, _engineerID, _level)); }
            catch (DalDoesNotExistException e) { Console.WriteLine(e.Message + "\n"); }

        }
    }
    /// <summary>
    /// input id of task and delete
    /// </summary>
    public static void DeleteTask()
    {
        Console.WriteLine("Enter Id to delete");
        int.TryParse(Console.ReadLine(), out int _idToDelete);
        try
        {
            s_dal!.Task.Delete(_idToDelete);
        }
        catch (DalDoesNotExistException e) { Console.WriteLine(e.Message + "\n"); }

    }
    /// <summary>
    /// display the option of engineer menu and do the user choice
    /// </summary>
    /// <returns>the user choice</returns>
    public static int TaskMenu()
    {
        int myChoice = WriteInnerMenue();
        switch (myChoice)
        {
            case 1:
                break;
            case 2:
                CreateTask();
                break;
            case 3:
                DisplayTask();
                break;
            case 4:
                DisplayAllTasks();
                break;
            case 5:
                UpdateTask();
                break;
            case 6:
                DeleteTask();
                break;
        }
        return myChoice;
    }
    /// <summary>
    /// input id of dependency and delete
    /// </summary>
    public static void DeleteDependency()
    {
        Console.WriteLine("Enter Id to delete");
        int.TryParse(Console.ReadLine(), out int _idToDelete);
        try
        {
            s_dal.Dependency!.Delete(_idToDelete);
        }
        catch (DalDoesNotExistException e) { Console.WriteLine(e.Message + "\n"); }
    }
    /// <summary>
    /// input details of new dependency and create
    /// </summary>
    public static void CreateDependency()
    {
        int _idOfFirstTask;
        Console.WriteLine("Create Dependency \ntype ID of first task");
        int.TryParse(Console.ReadLine(), out _idOfFirstTask);
        while (s_dal!.Dependency.Read(_idOfFirstTask) is null)
        {
            Console.WriteLine("Enter ID of first task");
            int.TryParse(Console.ReadLine(), out _idOfFirstTask);
        }//
        int _idOfSecondTask;
        Console.WriteLine("type ID of second task\n");
        int.TryParse(Console.ReadLine(), out _idOfSecondTask);
        while (s_dal!.Task.Read(_idOfSecondTask) is null)
        {
            Console.WriteLine("Enter ID of second task\n");
            int.TryParse(Console.ReadLine(), out _idOfSecondTask);
        }
        int id = s_dal!.Dependency.Create(new(0, _idOfFirstTask, _idOfSecondTask));
        Console.WriteLine(id + "\n");


    }
    /// <summary>
    /// input id of Dependency and display his details
    /// </summary>
    public static void DisplayDependency()
    {
        Console.WriteLine("Enter Id to search");
        int.TryParse(Console.ReadLine(), out int _idToSearch);
        Dependency? findDependency = s_dal!.Dependency.Read(_idToSearch);
        if (findDependency is not null)
            Console.WriteLine(findDependency);
        else Console.WriteLine("There is no id dependency");
    }
    /// <summary>
    /// diplay all dependencies
    /// </summary>
    public static void DisplayAllDependencies()
    {
        List<Dependency?> allDependencies = s_dal.Dependency!.ReadAll().ToList();
        foreach (Dependency? dependency in allDependencies)
            Console.WriteLine(dependency + "\n");
    }
    /// <summary>
    /// input id of Dependency, his details and update, his details and update
    /// </summary>
    public static void UpdateDependency()
    {
        Console.WriteLine("Enter Id to update");
        int.TryParse(Console.ReadLine(), out int _idToUpDate);
        Dependency? dependencyToUpdate = s_dal!.Dependency.Read(_idToUpDate);
        if (dependencyToUpdate is null)
        {
            Console.WriteLine("the id not exist");
        }
        else
        {
            Console.WriteLine(dependencyToUpdate);
            Console.WriteLine("Up Date Dependency \ntype ID of first task");
            int.TryParse(Console.ReadLine(), out int _idOfFirstTask);
            if (_idOfFirstTask==0)
                _idOfFirstTask=dependencyToUpdate.DependentTask;
            else
                while (s_dal!.Task.Read(_idOfFirstTask) is null)
                {
                    Console.WriteLine("type ID of first task");
                    int.TryParse(Console.ReadLine(), out _idOfFirstTask);
                }
            Console.WriteLine("Create Dependency \ntype ID of second task");
            int.TryParse(Console.ReadLine(), out int _idOfSecondTask);
            if(_idOfSecondTask==0)
                _idOfSecondTask=dependencyToUpdate.DependOnTask;
            else
                while (s_dal!.Task.Read(_idOfSecondTask) is null)
                {
                     Console.WriteLine("Enter ID of second task");
                    int.TryParse(Console.ReadLine(), out _idOfSecondTask);
                }
            try { s_dal!.Dependency.Update(new(_idToUpDate, _idOfFirstTask, _idOfSecondTask)); }
            catch (DalDoesNotExistException e) { Console.WriteLine(e.Message + "\n"); }
        }
    }
    /// <summary>
    /// display the option of dependency menu and do the user choice
    /// </summary>
    /// <returns>the user choice</returns>
    public static int DependencyMenu()
    {
        int myChoice = WriteInnerMenue();
        switch (myChoice)
        {
            case 1:
                break;
            case 2:
                CreateDependency();
                break;
            case 3:
                DisplayDependency();
                break;
            case 4:
                DisplayAllDependencies();
                break;
            case 5:
                UpdateDependency();
                break;
            case 6:
                DeleteDependency();
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
        try
        {
            Initialization.Do(s_dal);
            int myChoice = WriteMenu();
            while (myChoice != 0)
            {
                int innerChoice = 0;
                switch (myChoice)
                {
                    case 1:
                        innerChoice = EngineerMenu();
                        break;
                    case 2:
                        innerChoice = TaskMenu();
                        break;
                    case 3:
                        innerChoice = DependencyMenu();
                        break;
                }
                if (innerChoice == 1)
                    break;
                myChoice= WriteMenu();
            }
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
}