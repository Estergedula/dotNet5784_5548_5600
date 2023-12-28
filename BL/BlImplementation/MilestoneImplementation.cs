using BlApi;
using BO;

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
        DO.Task? doTask = _dal.Task.Read(id);
        var n = from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == id))
                select _dal.Task.Read(d.DependOnTask);
        IEnumerable<TaskInList> tasksOfMilistone = from t in (from d in (_dal.Dependency.ReadAll((d) => d!.DependentTask == id))
                                                              select _dal.Task.Read(d.DependOnTask))
                                                   select new BO.TaskInList { Id = t.Id, Description = t.Description, Alias = t.Alias, Status = Status.Scheduled };
        return new Milestone
        {
            MileStoneId = doTask!.Id,
            Descriotion = doTask.Description,
            Alias = doTask.Alias,
            Status = Status.OnTrack/*ffffffffffff*/,
            CreatedAt = doTask.CreatedAt,
            BaselineStartDate = doTask.DeadLine/*hjbvhf*/,
            Start = doTask.Start,
            ForecastDate = doTask.ForecastDate,
            DeadLine = doTask.DeadLine,
            Complete = doTask.Complete,
            CompletionPercentage = 3.2/*fgfg*/,
            Remarks = doTask.Remarks,
            Dependecies = tasksOfMilistone.ToList()
        };
    }

    public void Update(int id)
    {

    }
}
