

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency copy=item with { Id = newId };
        DataSource.Dependencies.Add(copy);
        return copy.Id;
    }

    public void Delete(int id)
    {
        Dependency? DependencyToDelete = Read(id);
        if (DependencyToDelete is null)
            throw new Exception($"Engineer with ID={id} does not exist.");
        else DataSource.Dependencies.Remove(DependencyToDelete);
    }

    public Dependency? Read(int id)
    {
        if (DataSource.Dependencies.Find(dependency => dependency.Id == id) is not null)
            return DataSource.Dependencies.Find(dependency => dependency.Id == id);
        else return null;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency? dependcyToUpdate=Read(item.Id);
        if (dependcyToUpdate is null)
            throw new Exception($"Dependency with ID={item.Id} does not exist.");
        DataSource. Dependencies.Remove(dependcyToUpdate);
        Dependency dependency = new(item.Id,item.DependentTask,item.DependOnTask);
        DataSource.Dependencies.Add(dependency);
    }
}
