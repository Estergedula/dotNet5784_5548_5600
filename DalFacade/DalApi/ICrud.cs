using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T> where T : class
{
    /// <summary>
    /// Creates new Task object in DAL
    /// </summary>
    /// <param name="item">item of task to create</param>
    /// <returns></returns>
    int Create(T item);
    /// <summary>
    /// Reads entity task by its ID
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns></returns>
    T? Read(int id);
    /// <summary>
    /// Reads all tasks objects
    /// </summary>
    /// <returns>the whole list of the tasks</returns>
    IEnumerable<T?> ReadAll(Func<T?, bool>? filter = null);
    /// <summary>
    /// Updates a Task object
    /// </summary>
    /// <param name="item">object item of task to update</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    void Update(T item);
    /// <summary>
    /// Deletes a Task by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    void Delete(int id);
    /// <summary>
    /// Read a specific item by filter function
    /// </summary>
    /// <param name="filter">function to select an item by</param>
    /// <returns></returns>
    T? Read(Func<T?, bool> filter);
}
