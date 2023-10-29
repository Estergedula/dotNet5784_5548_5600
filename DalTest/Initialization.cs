namespace DalTest;
using DalApi;
using DO;
using System.Collections;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

public static class Initialization
{
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;
    private static ITask? s_dalTask;
    private static readonly Random s_rand = new();
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
        "Esty Shvartz",
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
        "Miri Kaner",
        "Suly Eler"
        };
        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) is not null);
            EngineerExperience _level= (EngineerExperience)s_rand.Next(0, 2);
            double doubleCost = s_rand.Next(0, 100)/100;
            double _cost = s_rand.Next(MIN_INTEGER_COST, MAX_INTEGER_COST)+doubleCost;
            string _email = _name+(int)(_id/10000000)+"@gmail.com";
            Engineer newEng = new(_id, _name, _email, _level,_cost);
            s_dalEngineer!.Create(newEng);
        }
    }
    private static void createTask()
    {
        List<Engineer> allEngineer = s_dalEngineer!.ReadAll();
        int engineerCount= allEngineer.Count;
        string[] letters = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
        string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        for (int i=0; i<100; i++)
        {
            string _description = letters[s_rand.Next(letters.Length)]+nums[s_rand.Next(nums.Length)]+letters[s_rand.Next(letters.Length)];
            string _alias= _description.Substring(0, 2);
            bool _milestone= s_rand.Next(0,1)==0?true:false;
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _createdAt = start.AddDays(s_rand.Next(range));
            range = (new DateTime(1,12,2040) - _createdAt).Days;
            DateTime _start = _createdAt.AddDays(s_rand.Next(range));
            range = (new DateTime(1, 12, 2060) - _start).Days;
            DateTime _scheduledDate = _start.AddDays(s_rand.Next(range));
            range= (new DateTime(1, 12, 2080)-_scheduledDate).Days;
            DateTime _forecadtDate = _scheduledDate.AddDays(s_rand.Next(range));
            range=(_forecadtDate-_scheduledDate).Days;
            DateTime _complete = _scheduledDate.AddDays(s_rand.Next(range));
            Engineer engineerDoTask=allEngineer[s_rand.Next(0, engineerCount-1)];
            int _engineerId = engineerDoTask.Id;
            EngineerExperience _complexilyLevel = engineerDoTask.Level;
            Task newTask = new(0, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecadtDate, _complete, " ", " ", _engineerId, _complexilyLevel);
            s_dalTask!.Create(newTask);
        }
    }
    private static void createDependcy()
    {

    }
    public static void Do(IEngineer? dalEngineer,ITask? dalTask,IDependency? dalDependency)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask=dalTask?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency=dalDependency??throw new NullReferenceException("DAL can not be null!");
        createEngineer();
        createTask();
        createDependcy();
    }
}
