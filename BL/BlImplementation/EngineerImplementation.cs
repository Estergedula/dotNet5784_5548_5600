using BlApi;
using BO;
using System.Net.Mail;
using System.Runtime.CompilerServices;

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
            throw new Exception();
        DO.Engineer doEngineer = new DO.Engineer
        (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)(boEngineer.Level), boEngineer.Cost);
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new Exception(ex.Message);
            //throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if (GetTasksOfEngineer(id) == null) throw new Exception();
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    public Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new Exception();

        //  throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
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
    private TaskInEngineer? GetCurrentTaskOfEngineer(int idOfEngineer)
    {
        var allTasks = _dal.Task.ReadAll();
        TaskInEngineer? currentTaskInEngineer =
             (from t in allTasks
              where ((t.EngineerId == idOfEngineer) && (t.Start > DateTime.Now) && (t.Complete != DateTime.MinValue))
              select new TaskInEngineer { Id = t.Id, Name = t.Description }).FirstOrDefault();
        return currentTaskInEngineer;
    }
    private IEnumerable<TaskInEngineer> GetTasksOfEngineer(int idOfEngineer)
    {
        var allTasks = _dal.Task.ReadAll();
        IEnumerable<TaskInEngineer>? taskInEngineer =
            (from t in allTasks
             where (t.EngineerId == idOfEngineer)
             select new TaskInEngineer { Id = t.Id, Name = t.Description });
        return taskInEngineer;
    }

    public void Update(Engineer boEngineer)
    {
        if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Cost <= 0 || IsValidEmail(boEngineer.Email))
            throw new Exception();
        DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)(boEngineer.Level), boEngineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new Exception(ex.Message);
            //throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }


    public IEnumerable<Engineer> ReadAll(Func<BO.Engineer?, bool>? filter = null)
    {
        IEnumerable<DO.Engineer?> allEngineers = _dal.Engineer.ReadAll((Func<DO.Engineer?, bool>?)filter);
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
        return allEngineersinBo;
    }
}
