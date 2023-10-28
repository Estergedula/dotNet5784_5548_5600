namespace DalTest;
using DalApi;
using DO;
using System.Collections;

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
