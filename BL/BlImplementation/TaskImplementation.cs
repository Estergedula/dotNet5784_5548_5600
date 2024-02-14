namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        if (boTask.ScheduleDate < boTask.CreatedAt ||
        boTask.DeadLine < boTask.Complete ||
        boTask.Alias == "")
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

        DO.Task doTask = new(
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
            ForecastDate = DateTime.Now,
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
            ForecastDate = DateTime.Now,
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
        if (boTask.ScheduleDate < boTask.CreatedAt ||
        boTask.DeadLine < boTask.Complete ||
        boTask.Alias == "")
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        try
        {
            _dal.Task.Read(boTask.Engineer!.Id);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boTask.Engineer!.Id} does not exixt ");
        }
        DO.Task doTask = new
        (boTask.Id, boTask.Description, boTask.Alias, false, boTask.CreatedAt, boTask.Start,
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

