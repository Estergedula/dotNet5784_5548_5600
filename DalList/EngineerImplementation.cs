


namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? engineerToDelete = Read(id);
        if (engineerToDelete is null)
            throw new Exception($"Engineer with ID={id} does not exist.");
        else DataSource.Engineers.Remove(engineerToDelete);
    }

    public Engineer? Read(int id)
    {
        if (DataSource.Engineers.Find(x => x.Id == id) is not null)
            return DataSource.Engineers.Find(x => x.Id == id);
        else return null;
    }

    public List<Engineer> ReadAll()
    {
        return DataSource.Engineers;    
    }

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
