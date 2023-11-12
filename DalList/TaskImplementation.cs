

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates new Task object in DAL
    /// </summary>
    /// <param name="item">item of task to create</param>
    /// <returns></returns>
    public int Create(Task item)
    {
        int newID = DataSource.Config.NextTaskId;
        Task copy = item with { Id=newID };
        DataSource.Tasks.Add(copy);
        return copy.Id;
    }
    /// <summary>
    /// Deletes a Task by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    public void Delete(int id)//Deletes a Task by his Id
    {
        Task? TaskToDelete = Read(id);
        if (TaskToDelete is null)
            throw new DalDoesNotExistException($"Task with ID = {id} does not exsist.");
        else { 
            DataSource.Tasks.Remove(TaskToDelete);
            Task copy = TaskToDelete with { Complete=DateTime.Now };
            DataSource.Tasks.Add(copy);
        }
    }
    /// <summary>
    /// Reads entity task by its ID
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns></returns>

    public Task? Read(int id) 
    {
        if (DataSource.Tasks.Find(task => task.Id == id) is not null)
            return DataSource.Tasks.Find(task => task.Id == id);
        else return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all tasks objects
    /// </summary>
    /// <returns>the whole list of the tasks</returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        return new List<Task>(DataSource.Tasks);
    }
    /// <summary>
    /// Updates Task object
    /// </summary>
    /// <param name="item">object item of task to update</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    public void Update(Task item) 
    {
        Task? taskToUpdate= Read(item.Id)??throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist.");
        DataSource.Tasks.Remove(taskToUpdate);
        Task task = new(item.Id, item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ForecastDate, item.DeadLine, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexilyLevel);
        DataSource.Tasks.Add(task);
    }
}
