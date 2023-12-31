using BlApi;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.Milestone CreateLUZ(int id)
    {
        IEnumerable<DO.Dependency?> dependencies = _dal.Dependency.ReadAll();
        var groupByDependencies = dependencies.GroupBy(dependency => dependency!.DependOnTask,
            (dependencyOnTask, dependencies) =>new { Key = dependencyOnTask, Dependencies = dependencies.Select(dependency=>dependency!.DependentTask) }).Order();
        var groupByDependenciesNotDistinct = groupByDependencies.Distinct();
        var x = from dependency in groupByDependencies select new { };
        //var x=groupByDependenciesNotDistinct.Select(gruopOfDependencies => gruopOfDependencies.Dependencies.Select(dependency => new DO.Dependency(dependency, gruopOfDependencies.Key)));
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
    
    public BO.Milestone? Read(int id)
    {
        //Do we need to check bool milestone
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null) throw new BO.BlDoesNotExistException($"A milestone with ID number = {id} does not exist.");
        //.יש לבדוק האם מתודות הקריאה בשכבת הנתונים מחזירות שגיאות
        IEnumerable<DO.Task>tasksWhichDepentOnMe = from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == id))
                select _dal.Task.Read(d.DependOnTask);

        IEnumerable<BO.TaskInList> tasksOfMilestone = from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == id))
              let task =_dal.Task.Read(d.Id)
              select new BO.TaskInList { Id = task.Id, Description = task.Description, Alias = task.Alias, Status = BO.Status.Scheduled/*====*/ };
        return new BO.Milestone
        {
            MileStoneId = doTask!.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            Status = getStatuesOfTask(doTask),
            CreatedAt = doTask.CreatedAt,
            Start = doTask.Start,
            ForecastDate =DateTime.Now /*doTask.ForecastDate,*/,
            DeadLine = doTask.DeadLine,
            Complete = doTask.Complete,
            CompletionPercentage = getCompletionPercentage(tasksOfMilestone),
            Remarks = doTask.Remarks,
            Dependecies = tasksOfMilestone.ToList()
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
    private double getCompletionPercentage(IEnumerable<BO.TaskInList> tasksOfMilestone)
    {
        int numOfStartTasks = tasksOfMilestone.Sum((taskOdMilstone) => taskOdMilstone.Status==BO.Status.Unscheduled/*===*/? 1:0);
        int numOfAllDependiesTasks = tasksOfMilestone.Count();
        return (numOfStartTasks/numOfAllDependiesTasks)*100;
    }
    public BO.Milestone Update(BO.Task task)
    {
        if (task.Alias == "" || task.Description == "")
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        try
        {
            //?לבדוק האם זה אבן דרך? איפה מקבלים את הערכים
            DO.Task taskMilestone = _dal.Task.Read(task.Id)!;
            DO.Task updateMilestone = taskMilestone with { Alias = task.Alias, Description = task.Description, Remarks = task.Remarks };
            _dal.Task.Update(updateMilestone);
            IEnumerable<BO.TaskInList> tasksOfMilestone = from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == task.Id))
                                                       let taskOfMilestone = _dal.Task.Read(d.Id)
                                                       select new BO.TaskInList { Id = taskOfMilestone.Id, Description = taskOfMilestone.Description, Alias = taskOfMilestone.Alias, Status = getStatuesOfTask(taskOfMilestone)/*====*/ };
            return new BO.Milestone
            {
                Description = updateMilestone.Description,
                Alias = updateMilestone.Alias,
                Remarks = updateMilestone.Remarks
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Milestone with ID={task.Id} is not exists", ex);
        };

    }

    public BO.Milestone CreateLUZ()
    {
        throw new NotImplementedException();
    }
}
