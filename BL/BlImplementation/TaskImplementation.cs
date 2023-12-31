﻿namespace BlImplementation;

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
        boTask?.DependenciesList?.Select(task => new DO.Dependency(boTask.Id, task.Id));
        DO.Task doTask = new DO.Task(
            boTask!.Id, boTask.Description, boTask.Alias, false, boTask.CreatedAt,
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

    //     ---- task רשימת השדות של ----
    // 1. public int Id 
    // 2. public required string Description 
    // 3. public string? Alias 
    // 4. public MillestoneInTask? Milestone 
    // 5. public Status Status 
    // 6. public IEnumerable<TaskInList>? DependenciesList 
    // 7. public DateTime CreatedAt //תאריך יצירה
    // 8. public DateTime BaselineStartDate //תאריך התחלה משוער
    // 9. public DateTime Start //תאריך התחלה בפועל
    // 10. public DateTime ForecastDate //תאריך משוער לסיום
    // 11. public DateTime DeadLine //תאריך אחרון לסיום
    // 12. public DateTime Complete //תאריך סיום בפועל
    // 13. public string? Deliverables 
    // 14. public string? Remarks 
    // 15. public EngineerInTask? Engineer 
    // 16. public EngineerExperience ComplexilyLevel
    // 17. public DateTime RegistrationDate
    //================================
//    public int Id { get; init; }
//    public required string Description { get; set; }
//    public string? Alias { get; set; }
//    public MillestoneInTask? Milestone { get; set; }
//    public Status Status { get; set; }
//    public IEnumerable<TaskInList>? DependenciesList { get; set; }
//    public DateTime CreatedAt { get; set; }//תאריך יצירה
//    public DateTime BaselineStartDate { get; set; }//תאריך התחלה משוער
//    public DateTime Start { get; set; }//תאריך התחלה בפועל
//    public DateTime ForecastDate { get; set; }//תאריך משוער לסיום
//    public DateTime DeadLine { get; set; }//תאריך אחרון לסיום
//    public DateTime Complete { get; set; }//תאריך סיום בפועל
//    public string? Deliverables { get; set; }
//    public string? Remarks { get; set; }
//    public EngineerInTask? Engineer { get; set; }
//    public EngineerExperience ComplexilyLevel { get; set; }
//    public DateTime RegistrationDate { get; init; }

//}

public void Delete(int id)
    {
        if (Read(id) is null) throw new Exception();
        if 
        try
        {
            _dal.Task.Delete(id);
        }
        catch (Exception ex) { throw new Exception(); }
    }


    private BO.Status getStatuesOfTask(DO.Task task)
    {
       // BO.Status status = BO.Status.Scheduled;
        if (task.Id==1)
            return BO.Status.Scheduled;
        //else return BO.Status.Scheduled;
        /**/
        return BO.Status.OnTrack;
    }
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        BO.MillestoneInTask? milestomeInList = _dal.Task.ReadAll().Select(t => new BO.MillestoneInTask
        {
            Id = t!.Id,
            Alias = t.Alias
        }
        ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll((d) => d!.DependentTask == doTask!.Id && d.DependOnTask == t.Id)) is not null)).FirstOrDefault();
        return new BO.Task
        {
            Id = doTask!.Id,
            Description = doTask!.Description!,
            Alias = doTask!.Alias,
            Milestone = _dal.Task.ReadAll().Select(t => new BO.MillestoneInTask
            {
                Id = t!.Id,
                Alias = t.Alias
            }
        ).Where(doTask => (_dal.Task.Read(doTask.Id)!.Milestone && (_dal.Dependency.ReadAll((d) => d!.DependentTask == doTask!.Id && d.DependOnTask == doTask.Id)) is not null)).FirstOrDefault(),
            Status = getStatuesOfTask(doTask!),
            DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == doTask.Id).Select(d => new BO.TaskInList
            {
                Id = d!.Id,
                Alias = _dal.Task.Read(d.Id)!.Alias,
                Status = getStatuesOfTask(_dal.Task.Read(d.Id)!),
                Description = _dal.Task.Read(d.Id)!.Description
            }),
            CreatedAt = doTask!.CreatedAt,
            ScheduleDate=doTask.ScheduleDate,
            Start=doTask.Start,
            ForecastDate =DateTime.Now/*doTask.ForecastDate,*/,
            DeadLine =doTask.DeadLine,
            Complete =doTask.Complete,
            Deliverables =doTask.Deliverables,
            Remarks =doTask.Remarks,
            ComplexilyLevel=(BO.EngineerExperience)doTask.ComplexilyLevel
        };
    }
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool> filter)
    {
        IEnumerable<DO.Task?> allTasks = _dal.Task.ReadAll((Func<DO.Task?, bool>?)filter);
        IEnumerable<BO.Task> allTaskinBo = allTasks.Select(task => new BO.Task
        {
            Id = task!.Id,
            Description = task!.Description!,
            Alias = task!.Alias,
            Milestone = _dal.Task.ReadAll().Select(t => new BO.MillestoneInTask
            {
                Id = t!.Id,
                Alias = t.Alias
            }
        ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll((d) => d!.DependentTask == t!.Id && d.DependOnTask == t.Id)) is not null)).FirstOrDefault(),
            Status = getStatuesOfTask(task),
            DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask ==task.Id).Select(d => new BO.TaskInList
            {
                Id = d!.Id,
                Alias = _dal.Task.Read(d.Id)!.Alias,
                Status = getStatuesOfTask(_dal.Task.Read(d.Id)!),
                Description = _dal.Task.Read(d.Id)!.Description
            }),
            CreatedAt = task!.CreatedAt,
            ScheduleDate=task.ScheduleDate,
            Start=task.Start,
            ForecastDate =DateTime.Now/*doTask.ForecastDate,*/,
            DeadLine =task.DeadLine,
            Complete =task.Complete,
            Deliverables =task.Deliverables,
            Remarks =task.Remarks,
            ComplexilyLevel=(BO.EngineerExperience)task.ComplexilyLevel,
        });
        return allTaskinBo;
    }

    public void Update(BO.Task boTask)
    {
        if (boTask.Id <= 0 || boTask.Alias == "")
            throw new Exception();
        DO.Task doTask = new DO.Task
        (boTask.Id, boTask.Description, boTask.Alias, false/**/, boTask.CreatedAt, boTask.Start,
        boTask.ForecastDate, boTask.DeadLine, boTask.Complete, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexilyLevel);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new Exception(ex.Message);
            //throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }
}

