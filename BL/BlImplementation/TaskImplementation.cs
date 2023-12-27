using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    //int Id,
    //string? Description,
    //string? Alias,
    //bool Milestone,
    //DateTime CreatedAt,
    //DateTime Start,
    //DateTime ForecastDate,
    //DateTime DeadLine,
    //DateTime Complete,
    //string? Deliverables = null,
    //string? Remarks = null,
    //int EngineerId = 0,
    //EngineerExperience ComplexilyLevel = EngineerExperience.Junior,
    //bool isActive = true
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        if (boTask.Id <= 0 || boTask.Alias == "")
            throw new Exception();
        //var bh=new DO.Task(1,"gfgbg","gfhh",)
        Status status = new Status();
        boTask?.DependenciesList?.Select(task => new DO.Dependency(boTask.Id, task.Id));
        DO.Task doTask = new DO.Task(
            boTask!.Id,/* boTask.Description*/"hbyh", boTask.Alias, (boTask.Milestone) is not null ? true : false, boTask.CreatedAt,
             boTask.Start, boTask.ForecastDate,
             boTask.DeadLine, boTask.Complete, boTask.Deliverables, boTask.Remarks,
             boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexilyLevel, true);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // throw new BO.BlAlreadyExistsException($"Task with ID={boStudent.Id} already exists", ex);
            throw new Exception(ex.Message);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }


    private Status getStatuesOfTask(DO.Task task)
    {
        /**/
        return Status.OnTrack;
    }
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        var milestomeInList = _dal.Task.ReadAll().Select(t => new MilestomeInList
        {
            Id = t!.Id,
            Alias = t.Alias,
            Description = t.Description,
            CreatedAt = t.CreatedAt,
            CompletionPercentage = 33/* t.Complete[] double*/ ,
            Status = getStatuesOfTask(t)/**/
        }
        ).Where(t => (_dal.Task.Read(t.Id)!.Milestone &&(_dal.Dependency.ReadAll((d)=>d!.DependentTask==doTask!.Id&& d.DependOnTask == t.Id))is not null)).FirstOrDefault();
       return new BO.Task
        {
            Id = doTask!.Id,
            Description = doTask!.Description,
            Alias = doTask!.Alias,
            Milestone = milestomeInList,
            Status = getStatuesOfTask(doTask),
            DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == id).Select(d => new TaskInList 
            {Id=d!.Id,Alias= _dal.Task.Read(d.Id)!.Alias,Status= getStatuesOfTask(_dal.Task.Read(d.Id)!),Description= _dal.Task.Read(d.Id)!.Description
            })
        };
    }
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool> filter)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }

}
