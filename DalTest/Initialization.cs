using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        "Riki Kan",
        "Miri Kan",
        "Shira Choen",
        "Rivka Choen",
        "Ester Gedula Levi",
        "Noami Choen",
        "Dina Choen",
        "Racheli Choen",
        "Moni Mini",
        "Riki Pinchasi",
        "Dina Levi"
        };
        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);
            EngineerExperience _level= (EngineerExperience)s_rand.Next(0, 2);
            double doubleCost = s_rand.Next(0, 100)/100;
            double _cost = s_rand.Next(MIN_INTEGER_COST, MAX_INTEGER_COST)+doubleCost;
            string _email = _name+_id%10000000+"@gmail.com";
            Engineer newEng = new(_id, _name, _email, _level,_cost);
            s_dalEngineer!.Create(newEng);
        }
    }
}
