using BlApi;
using BO;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public Milestone CreateLUZ(int id)
    {
        throw new NotImplementedException();
    }
    //public int MileStoneId { get; init; }
    //public string? Descriotion { get; init; }
    //public string? Alias { get; init; }
    //public Status Status { get; set; }
    //public DateTime? CreatedAt { get; set; }
    //public DateTime? BaselineStartDate { get; set; }
    //public DateTime? Start { get; set; }
    //public DateTime? ForecastDate { get; set; }
    //public DateTime? DeadLine { get; set; }
    //public DateTime? Complete { get; set; }
    //public double? CompletionPercentage { get; set; }
    //public string? Remarks { get; set; }
    public List<BO.TaskInList>? dependecies { get; set; }
    public Milestone? Read(int id)
    {
        //is we need to chake bool milistone
        DO.Task? doTask = _dal.Task.Read(id);
        var n = from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == id))
                select _dal.Task.Read(d.DependOnTask);
        IEnumerable<TaskInList> tasksOfMilistone = from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == id))
              let task =_dal.Task.Read(d.Id)
              select new BO.TaskInList { Id = task.Id, Description = task.Description, Alias = task.Alias, Status = BO.Status.Scheduled/*====*/ };
        return new Milestone
        {
            MileStoneId = doTask!.Id,
            Descriotion = doTask.Description,
            Alias = doTask.Alias,
            Status = Status.OnTrack/*ffffffffffff*/,
            CreatedAt = doTask.CreatedAt,
            Start = doTask.Start,
            ForecastDate =DateTime.Now /*doTask.ForecastDate,*/,
            DeadLine = doTask.DeadLine,
            Complete = doTask.Complete,
            CompletionPercentage = getCompletionPercentage(tasksOfMilistone),
            Remarks = doTask.Remarks,
            Dependecies = tasksOfMilistone.ToList()
        };
    }
    private double getCompletionPercentage(IEnumerable<TaskInList> tasksOfMilistone)
    {
        int numOfStartTasks = tasksOfMilistone.Sum((taskOdMilstone) => taskOdMilstone.Status==BO.Status.Unscheduled/*===*/? 1:0);
        int numOfAllDependiesTasks = tasksOfMilistone.Count();
        return (numOfStartTasks/numOfAllDependiesTasks)*100;
    }
    public void Update(int id)
    {

    }
}
