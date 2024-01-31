using BlApi;
using BO;
using System.Net.Mail;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{

    private DalApi.IDal _dal = DalApi.Factory.Get;
    //DO==============
    //int Id,
    //   string? Name = null,
    //   string? Email = null,
    //   EngineerExperience Level = EngineerExperience.Junior,
    //   double? Cost = 0
    //BO==========
    //public int Id { get; init; }
    //public string? Name { get; set; }
    //public string? Email { get; set; }
    //public EngineerExperience Level { get; set; }
    //public double? Cost { get; set; }
    //public int? CurrentTaskId { get; set; }
    private bool IsValidEmail(string? email)
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
    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Cost <= 0 || IsValidEmail(boEngineer.Email))
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        DO.Engineer doEngineer = new DO.Engineer
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

    public void Delete(int id)
    {
        if (GetTasksOfEngineer(id) is not null) throw new BO.BlDeletionImpossible($"An engineer with ID number = {id} cannot be deleted.");
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException($"An engineer with ID number = {id} does not exist.", ex); }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer is null) throw new BO.BlDoesNotExistException($"An engineer with ID number = {id} does not exist."); //יש לבדוק מה בנוגע לזריקת חריגות ממתודת קרחאה בשכבת הנתונים
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
    private BO.TaskInEngineer? GetCurrentTaskOfEngineer(int idOfEngineer)
    {
        var allTasks = _dal.Task.ReadAll();
        BO.TaskInEngineer? currentTaskInEngineer =
             (from t in allTasks
              where ((t.EngineerId == idOfEngineer) && (t.Start > DateTime.Now) && (t.Complete != DateTime.MinValue))
              select new BO.TaskInEngineer { Id = t.Id, Alias = t.Alias }).FirstOrDefault();
        return currentTaskInEngineer;
    }
    private IEnumerable<BO.TaskInEngineer> GetTasksOfEngineer(int idOfEngineer)
    {
        var allTasks = _dal.Task.ReadAll();
        IEnumerable<BO.TaskInEngineer>? taskInEngineer =
            (from t in allTasks
             where (t.EngineerId == idOfEngineer)
             select new BO.TaskInEngineer { Id = t.Id, Alias = t.Alias });
        return taskInEngineer;
    }

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Cost <= 0 || !IsValidEmail(boEngineer.Email))
            throw new BO.BlInvalidDataException($"The data you entered is incorrect.");
        DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)(boEngineer.Level), boEngineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"An engineer with ID number = {boEngineer.Id} does not exist.", ex);
        }
    }


    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        //Delegate filterInDo =new Delegate(filter);
        //Func<DO.Engineer?, bool>?  newFilter=Tools.ConvertDelegate(filter, (delegate bool filter(DO.Engineer));
        //IEnumerable<DO.Engineer?> allEngineers = _dal.Engineer.ReadAll((Func<DO.Engineer?, bool>?)filter);
        //IEnumerable<BO.Engineer> allEngineersinBo = from doEngineer in allEngineers
        //                                            select new BO.Engineer
        //                                            {
        //                                                Id = doEngineer.Id,
        //                                                Name = doEngineer.Name,
        //                                                Email = doEngineer.Email,
        //                                                Level = (BO.EngineerExperience)doEngineer.Level,
        //                                                Cost = doEngineer.Cost,
        //                                                CurrentTask = GetCurrentTaskOfEngineer(doEngineer.Id)
        //                                            };
        //return allEngineersinBo;
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

        return filter==null? allEngineersinBo:allEngineersinBo.Where(filter);
    }
}
