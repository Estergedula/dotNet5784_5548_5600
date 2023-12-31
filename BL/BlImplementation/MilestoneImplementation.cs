using BlApi;
using BO;
using DO;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public Milestone CreateLUZ()
    {
        IEnumerable<Dependency?> dependencies = _dal.Dependency.ReadAll();
        var groupByDependencies = dependencies.GroupBy(dependency => dependency!.DependOnTask,
            (dependencyOnTask, dependencies) =>new { Key = dependencyOnTask, Dependencies = dependencies.Select(dependency=>dependency!.DependentTask) }).Order();
        var groupByDependenciesNotDistinct = groupByDependencies.Distinct();
        var x=groupByDependenciesNotDistinct.Select(gruopOfDependencies => gruopOfDependencies.Dependencies.Select(dependency => new DO.Dependency(dependency, gruopOfDependencies.Key)));
        
        //var c=hh.Select(hh => {hh.Key,hh.).ToList();
        //var x=from dependency in dependencies group dependency!.DependOnTask select new 
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
            Description = doTask.Description,
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
    private BO.Status getStatuesOfTask(DO.Task task)
    {

        if (task.ScheduleDate == DateTime.MinValue)
            return BO.Status.Unscheduled;
        else if (task.Start == DateTime.MinValue)
            return BO.Status.Scheduled;
        else if (task.DeadLine < DateTime.Now && task.Complete == DateTime.MinValue)
            return BO.Status.InJeopardy;
        else return BO.Status.OnTrack;
    }
    private double getCompletionPercentage(IEnumerable<TaskInList> tasksOfMilistone)
    {
        int numOfStartTasks = tasksOfMilistone.Sum((taskOdMilstone) => taskOdMilstone.Status==BO.Status.Unscheduled/*===*/? 1:0);
        int numOfAllDependiesTasks = tasksOfMilistone.Count();
        return (numOfStartTasks/numOfAllDependiesTasks)*100;
    }
    public BO.Milestone Update(BO.Task task)
    {
        if (task.Alias == "" || task.Description == "")
            throw new Exception();
        try
        {
            //לבדוק אם זה אבן דרך? אפה מקבלים את הערכים?
            DO.Task taskMilestone = _dal.Task.Read(task.Id)!;
            DO.Task updateMilistone = taskMilestone with { Alias = task.Alias, Description = task.Description, Remarks = task.Remarks };
            _dal.Task.Update(updateMilistone);
            IEnumerable<TaskInList> tasksOfMilistone = from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == task.Id))
                                                       let taskOfMilistone = _dal.Task.Read(d.Id)
                                                       select new BO.TaskInList { Id = taskOfMilistone.Id, Description = taskOfMilistone.Description, Alias = taskOfMilistone.Alias, Status = getStatuesOfTask(taskOfMilistone)/*====*/ };
            return new BO.Milestone
            {
                MileStoneId = updateMilistone.Id,
                Description = updateMilistone.Description,
                Alias = updateMilistone.Alias,
                CreatedAt = updateMilistone.CreatedAt,
                Status = getStatuesOfTask(updateMilistone),
                DeadLine = updateMilistone.DeadLine,
                Complete = updateMilistone.Complete,
                Remarks = updateMilistone.Remarks,
                CompletionPercentage = getCompletionPercentage(tasksOfMilistone),
                Dependecies = tasksOfMilistone.ToList(),
                ForecastDate = null
            };
        }
        catch (DO.DalDoesNotExistException)
        {
            // throw new BO.BlDoesNotExistException($"Milistone with ID={task.Id} is not exists");
            throw new Exception();
        };

    }
}
