namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates new Engineer object in DAL
    /// </summary>
    /// <param name="item">item of enginner to create</param>
    /// <returns></returns>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public int Create(Engineer item) 
    {
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }
    /// <summary>
    /// Deletes a Engineer by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public void Delete(int id)
    {
        Engineer? engineerToDelete = Read(id);
        if (engineerToDelete is null)
            throw new DalDoesNotExistException($"An engineer with ID number = {id} does not exist.");
        else DataSource.Engineers.Remove(engineerToDelete);
    }
    /// <summary>
    /// Reads entity Engineer by his ID
    /// </summary>
    /// <param name="id">id of the engineer to read</param>
    /// <returns>the object in engineers DB with this id</returns>
    public Engineer? Read(int id) 
    {
        return DataSource.Engineers.FirstOrDefault(engineer => engineer.Id == id);
    }
    /// <summary>
    /// Reads entity Engineer by a bool function
    /// </summary>
    /// <param name="filter">bool func to run each object</param>
    /// <returns>the first elment that return true to filter function</returns>
    public Engineer? Read(Func<Engineer?, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }
    /// <summary>
    /// Reads all engineer objects
    /// </summary>
    /// <returns>the whole list of the engineers</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Engineers.Select(engineer => engineer);
        else
            return DataSource.Engineers.Where(filter);
    }
    /// <summary>
    /// Updates an Engineer object
    /// </summary>
    /// <param name="item">object item of engineer to update</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public void Update(Engineer item) 
    {
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is null)
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exist.");
        DataSource.Engineers.Remove(item);
        Engineer engineer = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
        DataSource.Engineers.Add(engineer);
    }
}
