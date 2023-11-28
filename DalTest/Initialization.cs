namespace DalTest;
using DalApi;
using DO;
using System.Collections;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
public static class Initialization
{
    private static IDal? s_dal;
    private static readonly Random s_rand = new();
    /// <summary>
    /// Create random Engineers objects
    /// </summary>
    private static void createEngineer()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        const int MIN_INTEGER_COST = 29;
        const int MAX_INTEGER_COST = 1000;

        string[] engineerNames =
        {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
        "Shira Israelof",
        "Toiby Braish",
        "Maly Kibelevitz",
        "Ruti Salomon",
        "Dvory Mimran",
        "Sari Brodi",
        "Roizy Lefkovit",
        "Chani Rozinberg",
        "Ayala Shraber",
        "Chaya Klain",
        "Esty Ploit",
        "Pnini Cohen",
        "Giti Leder",
        "Feigy Haker",
        "Kaila Avramovitz",
        "Rachely Vainberg",
        "Gili Reker",
        "Zehava Simcha",
        "Nahama Levi",
        "Hindi Nachumi",
        "Leaha Segal",
        "Chaya Toyal",
        "Debbi Pety",
        "Anna Coheni",
        "Efrat Kati",
        "Devora Tal",
        "Tova Eliimelech",
        "Yeudit Avramov",
        "Sury Shvartz",
        "Malki Gotfrid",
        "Sari Brodi",
        "Roizy Safrin",
        "Eti Deblinger",
        "Racheli Bekerman",
        "Miri Kanner",
        "Shuly Eler"
        };
        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer.Read(_id) is not null);
            EngineerExperience _level= (EngineerExperience)s_rand.Next(1, 3);
            double doubleCost = s_rand.Next(0, 100)/100;
            double _cost = s_rand.Next(MIN_INTEGER_COST, MAX_INTEGER_COST)+doubleCost;
            string _email = _name+(int)(_id/10000000)+"@gmail.com";
            Engineer newEng = new(_id, _name, _email, _level,_cost);
            try
            {
                s_dal!.Engineer.Create(newEng);
            }
            catch(DalAlreadyExistsException e) {
                Console.WriteLine(e.Message);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
    /// <summary>
    /// Create random Tasks objects
    /// </summary>
    private static void createTask()
    {
        List<Engineer?> allEngineer = s_dal!.Engineer.ReadAll().ToList();
        int engineerCount= allEngineer.Count;
        string[] letters = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
        string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        for (int i = 0; i < 10; i++)
        {
            string _description = letters[s_rand.Next(letters.Length)] + nums[s_rand.Next(nums.Length)] + letters[s_rand.Next(letters.Length)];
            string _alias = _description.Substring(0, 2);
            bool _milestone = s_rand.Next(0, 1) == 0 ? true : false;
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _createdAt = start.AddDays(s_rand.Next(range));
            range = (new DateTime(2040, 12, 1) - _createdAt).Days;
            DateTime _start = _createdAt.AddDays(s_rand.Next(range));
            range = (new DateTime(2060, 12, 1) - _start).Days;
            DateTime _scheduledDate = _start.AddDays(s_rand.Next(range));
            range = (new DateTime(2080, 12, 1) - _scheduledDate).Days;
            DateTime _forecadtDate = _scheduledDate.AddDays(s_rand.Next(range));
            range = (_forecadtDate - _scheduledDate).Days;
            DateTime _complete = _scheduledDate.AddDays(s_rand.Next(range));
            Engineer? engineerDoTask = allEngineer[s_rand.Next(0, engineerCount - 1)];
            int _engineerId = engineerDoTask!.Id;
            EngineerExperience _complexilyLevel = engineerDoTask.Level;
            Task newTask = new(0, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecadtDate, _complete, " ", " ", _engineerId, _complexilyLevel);
            s_dal.Task!.Create(newTask);
        }
    }
    /// <summary>
    /// Create random Dependencies objects
    /// </summary>
    private static void createDependency()
    {
        List<Task?> allTasks = s_dal!.Task.ReadAll().ToList();
        for (int i = 0; i < 20; i++)
        {
            Dependency dependency = new(0, allTasks[s_rand.Next(allTasks.Count - 1)]!.Id, allTasks[s_rand.Next(allTasks.Count - 1)]!.Id);
            s_dal!.Dependency.Create(dependency);
        }
    }
    /// <summary>
    /// create the lists of each class and restart them
    /// </summary>
    /// <param name="dalEngineer">the interface of dal engineer</param>
    /// <param name="dalTask">the interface of dal task</param>
    /// <param name="dalDependency">the interface of dal dependency</param>
    /// <exception cref="NullReferenceException"></exception>
    public static void Do(IDal dal)
    {
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!");
        createEngineer();
        createTask();
        createDependency();
    }
}
