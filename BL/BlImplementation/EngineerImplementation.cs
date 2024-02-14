using BlApi;
using System.Net.Mail;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// A function that checks whether the email address entered is a valid email address
    /// </summary>
    /// <param name="email">The email adress which entered as a input for validation</param>
    /// <returns></returns>
    private static bool IsValidEmail(string? email)
    {
        bool valid = true;
        try
        {
            var emailAddress = new MailAddress(email ?? " ");
        }
        catch
        {
            valid = false;
        }

        return valid;
    }

    /// <summary>
    /// Creates new Engineer object in DAL
    /// </summary>
    /// <param name="boEngineer">The BO engineer type entity which recieved for creation</param>
    /// <returns>Id of the engineer created in DAL</returns>
    /// <exception cref="BO.BlInvalidDataException">Thrown if invalid data was received as input</exception>
    /// <exception cref="BO.BlAlreadyExistsException">Thrown if an attempt was made to create an engineer that already exists</exception>
    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Cost <= 0 || !IsValidEmail(boEngineer.Email))
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        if (boEngineer.CurrentTask!.Id != 0)
            try
            {
                if (_dal.Task.Read(boEngineer.CurrentTask!.Id) is null) throw new BO.BlDoesNotExistException("");
            }
            catch (BO.BlDoesNotExistException)
            {
                throw new BO.BlInvalidDataException($"Current task with ID={boEngineer.CurrentTask!.Id} does not exixt ");
            }
        try
        {
            DO.Task currentTask = _dal.Task.Read(boEngineer.CurrentTask!.Id)!;
            DO.Task copyCurrentTask = currentTask with { EngineerId = boEngineer!.Id } as DO.Task;
            _dal.Task.Update(copyCurrentTask);
        }
        catch
        {
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        }
        DO.Engineer doEngineer = new
        (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)(boEngineer.Level), boEngineer.Cost);
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);

            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    /// <summary>
    /// Deletes a Engineer by his Id
    /// </summary>
    /// <param name="id">The identification number of the engineer accepted for deletion</param>
    /// <exception cref="BO.BlDeletionImpossible">Thrown if the id of the engineer accepted for deletion belongs to an engineer whose deletion is impossible</exception>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the id of the engineer accepted for deletion belongs to an engineer who does not exist in the data layer</exception>
    public void Delete(int id)
    {
        if (GetTasksOfEngineer(id) is not null) throw new BO.BlDeletionImpossible($"An engineer with ID number = {id} cannot be deleted.");
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException($"An engineer with ID number = {id} does not exist.", ex); }
    }

    /// <summary>
    /// Reads entity Engineer by his ID
    /// </summary>
    /// <param name="id">Id of the engineer to read</param>
    /// <returns>The BO engineer type entity who created from the asked engineer in the data layer</returns>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the id of the engineer accepted for reading belongs to an engineer who does not exist in the data layer</exception>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id) ?? throw new BO.BlDoesNotExistException($"An engineer with ID number = {id} does not exist.");
        return new BO.Engineer
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            CurrentTask = GetCurrentTaskOfEngineer(id)
        };
    }

    /// <summary>
    /// Get the current task of a specific engineer
    /// </summary>
    /// <param name="idOfEngineer">The id of the engineer for whom you want to receive the current task</param>
    /// <returns>A BO task type entity which is the current task of the engineer</returns>
    private BO.TaskInEngineer? GetCurrentTaskOfEngineer(int idOfEngineer)
    {
        var allTasks = _dal.Task.ReadAll();
        BO.TaskInEngineer? currentTaskInEngineer =
             (from t in allTasks
              where ((t.EngineerId == idOfEngineer) && (t.Start > DateTime.Now) && (t.Complete != DateTime.MinValue))
              select new BO.TaskInEngineer { Id = t.Id, Alias = t.Alias }).FirstOrDefault();
        return currentTaskInEngineer;
    }

    /// <summary>
    /// Get the tasks of a specific engineer
    /// </summary>
    /// <param name="idOfEngineer">The id of the engineer for whom you want to receive his tasks</param>
    /// <returns>A set of the tasks of the engineer</returns>
    private IEnumerable<BO.TaskInEngineer> GetTasksOfEngineer(int idOfEngineer)
    {
        var allTasks = _dal.Task.ReadAll();
        IEnumerable<BO.TaskInEngineer>? taskInEngineer =
            (from t in allTasks
             where (t.EngineerId == idOfEngineer)
             select new BO.TaskInEngineer { Id = t.Id, Alias = t.Alias });
        return taskInEngineer;
    }

    /// <summary>
    /// Updates an Engineer object
    /// </summary>
    /// <param name="boEngineer">The BO engineer type entity which recieved for updation</param>
    /// <exception cref="BO.BlInvalidDataException">Thrown if invalid data was received as input</exception>
    /// <exception cref="BO.BlAlreadyExistsException">Thrown if an attempt was made to create a engineer that already exists</exception>
    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Cost <= 0 || !IsValidEmail(boEngineer.Email))
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        try
        {
            _dal.Task.Read(boEngineer.CurrentTask!.Id);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlInvalidDataException($"Current task with ID={boEngineer.CurrentTask!.Id} does not exixt ");
        }
        DO.Engineer doEngineer = new(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)(boEngineer.Level), boEngineer.Cost);
        if (boEngineer.CurrentTask!.Id != 0)
            try
            {
                DO.Task currentTask = _dal.Task.Read(boEngineer.CurrentTask!.Id)!;
                DO.Task copyCurrentTask = currentTask with { EngineerId = boEngineer!.Id } as DO.Task;
                _dal.Task.Update(copyCurrentTask);
            }
            catch
            {
                throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
            }
        try
        {

            _dal.Engineer.Update(doEngineer);

        }

        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"An engineer with ID number = {boEngineer.Id} does not exist.", ex);
        }
    }

    /// <summary>
    /// Read all engineers who fulfill a certain condition for screening
    /// </summary>
    /// <param name="filter">The condition for screening</param>
    /// <returns>A set of the engineers who fulfill the condition</returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        IEnumerable<DO.Engineer?> allEngineers = _dal.Engineer.ReadAll();
        IEnumerable<BO.Engineer> allEngineersinBo = from doEngineer in allEngineers
                                                    select new BO.Engineer
                                                    {
                                                        Id = doEngineer.Id,
                                                        Name = doEngineer.Name,
                                                        Email = doEngineer.Email,
                                                        Level = (BO.EngineerExperience)doEngineer.Level,
                                                        Cost = doEngineer.Cost,
                                                        CurrentTask = GetCurrentTaskOfEngineer(doEngineer.Id)
                                                    };

        return filter == null ? allEngineersinBo : allEngineersinBo.Where(filter);
    }
}
