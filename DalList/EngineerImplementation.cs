


namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item) //Creates new Engineer object in DAL
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id) //Deletes a Engineer by its Id
    {
        Engineer? engineerToDelete = Read(id);
        if (engineerToDelete is null)
            throw new Exception($"Engineer with ID={id} does not exist.");
        else DataSource.Engineers.Remove(engineerToDelete);
    }

    public Engineer? Read(int id) //Reads entity Engineer by its ID 
    {
        if (DataSource.Engineers.Find(engineer => engineer.Id == id) is not null)
            return DataSource.Engineers.Find(engineer => engineer.Id == id);
        else return null;
    }

    public List<Engineer> ReadAll()//Reads all Engineers objects
    {
        return new List<Engineer>(DataSource.Engineers);    
    }

    public void Update(Engineer item) //Updates Engineer object
    {
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is null)
            throw new Exception($"Engineer with ID={item.Id} does not exist.");
        DataSource.Engineers.Remove(engineerToUpdate);
        Engineer engineer = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
        DataSource.Engineers.Add(engineer);
    }
}
