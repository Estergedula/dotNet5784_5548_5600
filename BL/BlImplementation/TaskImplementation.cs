

using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        //if (item.Id<=0||item.Alias=="")
        //    throw new Exception();
        //DO.Task doTask = new DO.Task
        //    (boTask.Id, boTask.Name, boStudent.Alias, boStudent.IsActive, boStudent.BirthDate);
        //try
        //{
        //    i
        //nt idStud = _dal.Student.Create(doStudent);
        //    return idStud;
        //}c
        //atch(DO.DalAlreadyExistsException ex)
        //{
        //    t
        //hrow new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        //}

        //???????????
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
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
