

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates new Dependency object in DAL
    /// </summary>
    /// <param name="item">new item to create in DB</param>
    /// <returns>the id of the item</returns>
    public int Create(Dependency item)
    {
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
            throw new DalDoesNotExistException($"Engineer with ID={id} does not exist.");
        else DataSource.Dependencies.RemoveAll(Dependency=>Dependency.Id==DependencyToDelete.Id);
    }
    /// <summary>
    /// Reads Dependency object by its ID 
    /// </summary>
    /// <param name="id">id of object to read</param>
    /// <returns></returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all Dependencies objects
    /// </summary>
    /// <returns>the whole list of the dependencies</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Dependencies.Select(depen => depen);
        else
            return DataSource.Dependencies.Where(filter);

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
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exist.");
        DataSource. Dependencies.RemoveAll(dependency => dependency.Id == item.Id);
        Dependency dependency = new(item.Id,item.DependentTask,item.DependOnTask);
        DataSource.Dependencies.Add(dependency);
    }   
}
