

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newID = DataSource.Config.NextTaskId;
        Task copy = item whith(Id = newID);
        DataSource.Tasks.Add(copy);
        return copy.Id;
    }

    public void Delete(int id)
    {
        Task? TaskToDelete = Read(id);
        if (TaskToDelete is null)
            throw new Exception($"Task with ID = {id} does not exsist.)
        else DataSource.Tasks.Remove(TaskToDelete);
    }

    public Task? Read(int id)
    {
        if (DataSource.Tasks.Find(x => x.Id == id) is not null)
            return DataSource.Tasks.Find(x => x.Id == id);
        else return null;
    }

    public List<Task> ReadAll()
    {
        return DataSource.Tasks;
    }

    public void Update(Task item)
    {
        Task? TaskToUpdate = Read(item.Id);
        if (TaskToUpdate is null)
            throw new Exception($"Task with ID = {id} does not exist.");
        DataSource.Tasks.Remove(TaskToUpdate);
        Task newTask = new(item.Status, item.Alias, item.MileStone, item.CretedAt, item.Start, item.ScheduledDate, item.ForecadtDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexilyLevel);
        DataSource.Tasks.Add.(newTask);
    }
}
