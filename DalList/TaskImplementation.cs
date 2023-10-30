

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)//Creates new Task object in DAL
    {
        int newID = DataSource.Config.NextTaskId;
        Task copy = item with { Id=newID };
        DataSource.Tasks.Add(copy);
        return copy.Id;
    }

    public void Delete(int id)//Deletes a Task by its Id
    {
        Task? TaskToDelete = Read(id);
        if (TaskToDelete is null)
            throw new Exception($"Task with ID = {id} does not exsist.");
        else DataSource.Tasks.Remove(TaskToDelete);
    }

    public Task? Read(int id) //Reads Task object by its ID 
    {
        if (DataSource.Tasks.Find(task => task.Id == id) is not null)
            return DataSource.Tasks.Find(task => task.Id == id);
        else return null;
    }

    public List<Task> ReadAll() // Reads all Tasks objects
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item) //Updates Task object
    {
        Task? taskToUpdate= Read(item.Id);
        if (taskToUpdate is null)
            throw new Exception($"Task with ID={item.Id} does not exist.");
        DataSource.Tasks.Remove(taskToUpdate);
        Task task = new(item.Id, item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ForecastDate, item.DeadLine, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexilyLevel);
        DataSource.Tasks.Add(task);
    }
}
