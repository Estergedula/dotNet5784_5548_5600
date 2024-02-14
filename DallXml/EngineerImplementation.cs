namespace Dal;
using DalApi;
using DallXml;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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
        if(Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        const string XMLENGINEER = "engineers";
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        list.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(list, XMLENGINEER);
        return item.Id;
    }
    /// <summary>
    /// Deletes a Engineer by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public void Delete(int id)
    {
        const string XMLENGINEER = @"engineers";
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        Engineer? taskToDelete = Read(id);
        if (taskToDelete is null)
            throw new DalDoesNotExistException($"Engineer with ID = {id} does not exsist.");
        else
        {
            list.Remove(taskToDelete);
            XMLTools.SaveListToXMLSerializer<Engineer>(list, XMLENGINEER);
        }
    }
    /// <summary>
    /// Reads entity Engineer by his ID
    /// </summary>
    /// <param name="id">id of the engineer to read</param>
    /// <returns>the object in engineers DB with this id</returns>

    public Engineer? Read(int id)
    {
        const string XMLENGINEER = "engineers";
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        return list.Find(engineerToReturn => engineerToReturn!.Id == id);
    }
    /// <summary>
    /// Reads entity Engineer by a bool function
    /// </summary>
    /// <param name="filter">bool func to run each object</param>
    /// <returns>the first elment that return true to filter function</returns>
    public Engineer? Read(Func<Engineer?, bool> filter)
    {
        const string XMLENGINEER = "engineers";
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        return list!.FirstOrDefault(filter);
    }
    /// <summary>
    /// Reads all engineer objects
    /// </summary>
    /// <returns>the whole list of the engineers</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        const string XMLENGINEER = "engineers";
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        if (filter == null)
            return list.Select(task => task);
        else return list!.Where<Engineer>(filter);
    }
    /// <summary>
    /// Updates an Engineer object
    /// </summary>
    /// <param name="item">object item of engineer to update</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public void Update(Engineer item)
    {
        const string XMLENGINEER = "engineers";
        List<Engineer> list = XMLTools.LoadListFromXMLSerializer<Engineer>(XMLENGINEER);
        Engineer? engineerToUpdate = Read(item.Id) ?? throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exist.");
        list.Remove(engineerToUpdate);
        Engineer engineer = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
        list.Add(engineer);
        XMLTools.SaveListToXMLSerializer<Engineer>(list, XMLENGINEER);
    }
}
