

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
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
    public void Delete(int id)//Deletes a Task by its Id
    {
        Task? TaskToDelete = Read(id);
        if (TaskToDelete is null)
            throw new Exception($"Task with ID = {id} does not exsist.");
        else DataSource.Tasks.Remove(TaskToDelete);
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
    /// Reads all tasks objects
    /// </summary>
    /// <returns>the whole list of the tasks</returns>
    public List<Task> ReadAll() 
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
        Task? taskToUpdate= Read(item.Id);
        if (taskToUpdate is null)
            throw new Exception($"Task with ID={item.Id} does not exist.");
        DataSource.Tasks.Remove(taskToUpdate);
        Task task = new(item.Id, item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ForecastDate, item.DeadLine, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexilyLevel);
        DataSource.Tasks.Add(task);
    }
}
