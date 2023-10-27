


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
        if (DataSource.Engineers.Find(x => x.Id == id) is null)
            throw new Exception($"Engineer with ID={id} is not exists");
        else DataSource.Engineers.Remove(Read(id));
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
        if (DataSource.Engineers.Find(x => x.Id == item.Id) is null)
            throw new Exception($"Engineer with ID={item.Id} is not exists");
        else
        {
            DataSource.Engineers.Remove(Read(item.Id));
            Engineer engineer = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
            DataSource.Engineers.Add(engineer);
        }
    }
}
