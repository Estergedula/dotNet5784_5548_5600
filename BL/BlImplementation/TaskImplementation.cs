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

    /// <summary>
    /// Creates new Task object in DAL
    /// </summary>
    /// <param name="boTask">The BO task type entity which recieved for creation</param>
    /// <returns>Id of the engineer created in DAL</returns>
    /// <exception cref="BO.BlInvalidDataException">Thrown if invalid data was received as input</exception>
    /// <exception cref="BO.BlAlreadyExistsException">Thrown if an attempt was made to create a task that already exists</exception>
    public int Create(BO.Task boTask)
    {
        if (boTask.Start > boTask.ScheduleDate || boTask.ScheduleDate > boTask.ForecastDate ||
            boTask.ForecastDate < boTask.Complete || boTask.DeadLine < boTask.Complete ||
            boTask.Id <= 0 || boTask.Alias == "")
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        try
        {
            _dal.Task.Read(boTask.Engineer!.Id);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlInvalidDataException($"ENginner with ID={boTask.Engineer!.Id} does not exixt ");
        }

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
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
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

    /// <summary>
    /// Deletes a Engineer by his Id
    /// </summary>
    /// <param name="id">The identification number of the task entity accepted for deletion</param>
    /// <exception cref="BO.BlDeletionImpossible">Thrown if the id of the task entity accepted for deletion belongs to an engineer whose deletion is impossible</exception>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the id of the task entity accepted for deletion belongs to an engineer who does not exist in the data layer</exception>
    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDeletionImpossible ex) { throw new BO.BlDeletionImpossible($"A task with ID number = {id} cannot be deleted.", ex); }
        catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException($"A task with ID number = {id} does not exist.", ex); }
    }

    /// <summary>
    /// Get the statues of a specifict task
    /// </summary>
    /// <param name="task">The DO task type entity for whom you want to receive its status</param>
    /// <returns>The status of the task who recieved</returns>
    private BO.Status GetStatuesOfTask(DO.Task task)
    {

        if (task.ScheduleDate == DateTime.MinValue)
            return BO.Status.Unscheduled;
        else if (task.Start == DateTime.MinValue)
            return BO.Status.Scheduled;
        else if (task.DeadLine < DateTime.Now && task.Complete == DateTime.MinValue)
            return BO.Status.InJeopardy;
        else return BO.Status.OnTrack;
    }

    /// <summary>
    /// Reads entity Task by his ID
    /// </summary>
    /// <param name="id">Id of the task to read</param>
    /// <returns>The BO engineer type entity who created from the asked task in the data layer</returns>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the id of the task accepted for reading belongs to an task who does not exist in the data layer</exception>
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null) throw new BO.BlDoesNotExistException($"A task with ID number = {id} does not exist.");
        BO.MillestoneInTask? milestomeInList = _dal.Task.ReadAll().Select(t => new BO.MillestoneInTask
        {
            Id = t!.Id,
            Alias = t.Alias
        }
        ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll((d) => d!.DependentTask == doTask!.Id && d.DependOnTask == t.Id)) is not null)).FirstOrDefault();
        DO.Engineer? engineerOfTask = _dal!.Engineer!.Read(doTask.EngineerId);
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
            Status = GetStatuesOfTask(doTask!),
            DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == doTask.Id).Select(d => new BO.TaskInList
            {
                Id = d!.Id,
                Alias = _dal.Task.Read(d.Id)!.Alias,
                Status = GetStatuesOfTask(_dal.Task.Read(d.Id)!),
                Description = _dal.Task.Read(d.Id)!.Description
            }),
            CreatedAt = doTask!.CreatedAt,
            ScheduleDate = doTask.ScheduleDate,
            Start = doTask.Start,
            ForecastDate = DateTime.Now/*doTask.ForecastDate,*/,
            DeadLine = doTask.DeadLine,
            Complete = doTask.Complete,
            Deliverables = doTask.Deliverables,
            Engineer = new BO.EngineerInTask { Id = doTask.EngineerId, Name = engineerOfTask!.Name },
            Remarks = doTask.Remarks,
            ComplexilyLevel = (BO.EngineerExperience)doTask.ComplexilyLevel
        };
    }

    /// <summary>
    /// Read all engineers who fulfill a certain condition for screening
    /// </summary>
    /// <param name="filter">The condition for screening</param>
    /// <returns>A set of the engineers who fulfill the condition</returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter = null)
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
            Status = GetStatuesOfTask(task),
            DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == task.Id).Select(d => new BO.TaskInList
            {
                Id = d!.Id,
                Alias = _dal.Task.Read(d.Id)!.Alias,
                Status = GetStatuesOfTask(_dal.Task.Read(d.Id)!),
                Description = _dal.Task.Read(d.Id)!.Description
            }),
            CreatedAt = task!.CreatedAt,
            ScheduleDate = task.ScheduleDate,
            Start = task.Start,
            ForecastDate = DateTime.Now/*doTask.ForecastDate,*/,
            DeadLine = task.DeadLine,
            Complete = task.Complete,
            Deliverables = task.Deliverables,
            Remarks = task.Remarks,
            ComplexilyLevel = (BO.EngineerExperience)task.ComplexilyLevel,
        });
        return allTaskinBo;
    }

    /// <summary>
    /// Updates a Task object
    /// </summary>
    /// <param name="boTask">The BO task type entity which recieved for updation</param>
    /// <exception cref="BO.BlInvalidDataException">Thrown if invalid data was received as input</exception>
    /// <exception cref="BO.BlAlreadyExistsException">Thrown if an attempt was made to create a task that already exists</exception>
    public void Update(BO.Task boTask)
    {
        if (boTask.Start > boTask.ScheduleDate || boTask.ScheduleDate< boTask.ForecastDate ||
             boTask.DeadLine < boTask.Complete ||boTask.DeadLine< boTask.ForecastDate||
            boTask.Id <= 0 || boTask.Alias == "" || boTask.Id <0)
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        try
        {
            _dal.Task.Read(boTask.Engineer!.Id);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boTask.Engineer!.Id} does not exixt ");
        }
        DO.Task doTask = new DO.Task
        (boTask.Id, boTask.Description, boTask.Alias, false/**/, boTask.CreatedAt, boTask.Start,
        boTask.ForecastDate, boTask.DeadLine, boTask.Complete, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexilyLevel);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boTask.Id} already exists", ex);
        }
    }
}

