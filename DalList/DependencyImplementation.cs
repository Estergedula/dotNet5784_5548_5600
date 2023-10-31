

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates new Dependency object in DAL
    /// </summary>
    /// <param name="item">item to create in DB</param>
    /// <returns>the id of the item</returns>
    public int Create(Dependency item)
    {
        List<Dependency> list = ReadAll();
        foreach (Dependency dependency in list)
        {
            if((dependency.DependentTask==item.DependentTask&&dependency.DependOnTask==item.DependOnTask)||(dependency.DependentTask==item.DependOnTask&&dependency.DependOnTask==item.DependentTask))
                throw new Exception("can not create this dependency");
            
        }

        int newId = DataSource.Config.NextDependencyId;
        Dependency copy=item with { Id = newId };
        DataSource.Dependencies.Add(copy);
        return copy.Id;
    }
    /// <summary>
    /// Deletes a Dependency by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the param id is not exist in the DB</exception>
    public void Delete(int id)

    {
        Dependency? DependencyToDelete = Read(id);
        if (DependencyToDelete is null)
            throw new Exception($"Engineer with ID={id} does not exist.");
        else DataSource.Dependencies.Remove(DependencyToDelete);
    }
    /// <summary>
    /// Reads Dependency object by its ID 
    /// </summary>
    /// <param name="id">id of object to read</param>
    /// <returns></returns>
    public Dependency? Read(int id)
    {
        if (DataSource.Dependencies.Find(dependency => dependency.Id == id) is not null)
            return DataSource.Dependencies.Find(dependency => dependency.Id == id);
        else return null;
    }
    /// <summary>
    /// Reads all Dependencies objects
    /// </summary>
    /// <returns>the whole list of the dependencies</returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }
    /// <summary>
    /// Updates Dependency object
    /// </summary>
    /// <param name="item">object item of dependcy to update</param>
    /// <exception cref="Exception">the input id of the dependency does not exist</exception>
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
