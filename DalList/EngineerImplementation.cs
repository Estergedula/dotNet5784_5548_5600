


namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

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
            throw new Exception($"Engineer with ID={item.Id} already exists");
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
            throw new Exception($"Engineer with ID={id} does not exist.");
        else DataSource.Engineers.Remove(engineerToDelete);
    }
    /// <summary>
    /// Reads entity Engineer by its ID
    /// </summary>
    /// <param name="id">id of the engineer to read</param>
    /// <returns></returns>
    public Engineer? Read(int id) 
    {
        if (DataSource.Engineers.Find(engineer => engineer.Id == id) is not null)
            return DataSource.Engineers.Find(engineer => engineer.Id == id);
        else return null;
    }
    /// <summary>
    /// Reads all engineers objects
    /// </summary>
    /// <returns>the whole list of the engineers</returns>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);    
    }
    /// <summary>
    /// Updates Engineer object
    /// </summary>
    /// <param name="item">object item of engineer to update</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public void Update(Engineer item) 
    {
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is null)
            throw new Exception($"Engineer with ID={item.Id} does not exist.");
        DataSource.Engineers.Remove(engineerToUpdate);
        Engineer engineer = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
        DataSource.Engineers.Add(engineer);
    }
}
