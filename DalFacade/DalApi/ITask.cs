namespace DalApi;
using DO;

public interface ITask
{
    /// <summary>
    /// Creates new Task object in DAL
    /// </summary>
    /// <param name="item">item of task to create</param>
    /// <returns></returns>
    int Create(Task item);
    /// <summary>
    /// Reads entity task by its ID
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns></returns>
    Task? Read(int id);
    /// <summary>
    /// Reads all tasks objects
    /// </summary>
    /// <returns>the whole list of the tasks</returns>
    List<Task> ReadAll();
    /// <summary>
    /// Updates Task object
    /// </summary>
    /// <param name="item">object item of task to update</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    void Update(Task item);
    /// <summary>
    /// Deletes a Task by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    void Delete(int id); 
}
