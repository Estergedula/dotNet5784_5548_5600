﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates new Task object in DAL
    /// </summary>
    /// <param name="item">item of task to create</param>
    /// <returns>the id of new object</returns>
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
    public void Delete(int id)
    {
        Task? taskToDelete = Read(id);
        if (taskToDelete is null)
            throw new DalDoesNotExistException($"Task with ID = {id} does not exist.");
        else DataSource.Tasks.Remove(taskToDelete);
    }
    /// <summary>
    /// Reads entity task by his ID
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns>the item with this id</returns>
    public Task? Read(int id) 
    {
        return DataSource.Tasks.FirstOrDefault(task => task.Id == id);
    }
    /// <summary>
    /// Reads entity task by a bool function
    /// </summary>
    /// <param name="filter">bool func to run each object</param>
    /// <returns>the first elment that return true to filter function</returns>
    public Task? Read(Func<Task?, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all task objects
    /// </summary>
    /// <returns>the whole list of the tasks</returns>
    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Tasks.Select(task => task);
        else return DataSource.Tasks.Where(filter);
    }
    /// <summary>
    /// Updates a Task object
    /// </summary>
    /// <param name="item">object item of task to update</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    public void Update(Task item) 
    {
        Task? taskToUpdate = Read(item.Id);
            if(taskToUpdate is null)
                throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist.");
        DataSource.Tasks.Remove(taskToUpdate);
        Task task = new(item.Id, item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ScheduleDate, item.DeadLine, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexilyLevel);
        DataSource.Tasks.Add(task);
    }
}
