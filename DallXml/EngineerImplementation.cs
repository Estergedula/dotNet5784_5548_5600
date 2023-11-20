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
        const string FILEADRESS= "..\\..\\..\\tasks";
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement(FILEADRESS);
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        listOfEngineers.Add(item);
        XMLTools.SaveListToXMLElement(listOfEngineers, FILEADRESS);
        return item.Id;
    }
    /// <summary>
    /// Deletes a Engineer by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Reads entity Engineer by his ID
    /// </summary>
    /// <param name="id">id of the engineer to read</param>
    /// <returns>the object in engineers DB with this id</returns>

    public Engineer? Read(int id)
    {
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement("..\\..\\..\\tasks");
        var engineerToReturn = from engineer in listOfEngineers.Elements("Address")
                where (int)engineer.Attribute("Id") == id
                select engineer;
        return (Engineer)engineerToReturn;
    }
    /// <summary>
    /// Reads entity Engineer by a bool function
    /// </summary>
    /// <param name="filter">bool func to run each object</param>
    /// <returns>the first elment that return true to filter function</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement("..\\..\\..\\tasks");
        var allEngineer = from engineerToAdd in listOfEngineers.Elements("Address")
                         // where filter((string)engineerToAdd.to(!.Attribute("Type") == "Billing"
                          select engineerToAdd;
        allEngineer=allEngineer.ToList();
        var engineerToReturn = allEngineer.Where(filter).ToList();
        return (Engineer)engineerToReturn;
    }
    /// <summary>
    /// Reads all engineer objects
    /// </summary>
    /// <returns>the whole list of the engineers</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        //XElement listOfEngineers = XMLTools.LoadListFromXMLElement("..\\..\\..\\tasks");
        //return DataSource.Engineers.FirstOrDefault(filter);
        return null;
    }
    /// <summary>
    /// Updates an Engineer object
    /// </summary>
    /// <param name="item">object item of engineer to update</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    public void Update(Engineer item)
    {
        const string FILEADRESS = "..\\..\\..\\tasks";
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement(FILEADRESS);
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is null)
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exist.");
      //  listOfEngineers.Elements.Remove(item);
        Engineer engineer = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
        listOfEngineers.Add(engineer);
        XMLTools.SaveListToXMLElement(listOfEngineers, FILEADRESS);
    }
}
