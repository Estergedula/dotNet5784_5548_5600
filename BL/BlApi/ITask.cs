namespace BlApi;

/// <summary>
/// A logical subinterface for a primary logical entity: Task
/// </summary>
public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool> ?filter=null);
    public void Update(BO.Task boTask);
    public void Delete(int id);
}
