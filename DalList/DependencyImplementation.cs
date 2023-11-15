﻿

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
        Dependency? dependencyToDelete = Read(id);
        if (dependencyToDelete is null)
            throw new DalDoesNotExistException($"Engineer with ID={id} does not exist.");
        else DataSource.Dependencies.RemoveAll(dependency=>dependency.Id==dependencyToDelete.Id);
    }
    /// <summary>
    /// Reads Dependency object by his ID 
    /// </summary>
    /// <param name="id">id of object to read</param>
    /// <returns>the object in engineers DB with this id</returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
    }
    /// <summary>
    /// Reads Dependency object by a bool function 
    /// </summary>
    /// <param name="filter">bool func to run each object</param>
    /// <returns>the first elment that return true to filter function</returns>
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
            return DataSource.Dependencies.Select(dependency => dependency);
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
