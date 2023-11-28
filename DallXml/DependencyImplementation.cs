
namespace Dal;
using DalApi;
using DallXml;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates a new Dependency object in DAL
    /// </summary>
    /// <param name="item">new item to create in DB</param>
    /// <returns>new item to create in DB</returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Dependency item)
    {
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"dependency with ID={item.Id} already exists");
        const string XMLDEPENDENCY = "dependencies";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        XElement dependencyToAdd = new("Dependency");
        int _newId = Config.nextDependencyId;
        XElement _id = new("Id");
        _id.Value = Convert.ToString(_newId);
        dependencyToAdd.Add(_id);
        XElement _dependentTask = new("DependentTask");
        _dependentTask.Value = Convert.ToString(item.DependentTask);
        dependencyToAdd.Add(_dependentTask);
        XElement _dependOnTask = new("DependOnTask");
        _dependOnTask.Value = Convert.ToString(item.DependOnTask);
        dependencyToAdd.Add(_dependOnTask);
        listOfDependencies.Add(dependencyToAdd);
        XMLTools.SaveListToXMLElement(listOfDependencies, XMLDEPENDENCY);
        return _newId;
    }

    /// <summary>
    /// Deletes a Dependency by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the param id is not exist in the DB</exception>
    public void Delete(int id)
    {
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist.");
        const string XMLDEPENDENCY = @"dependencies";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        XElement? dependencyToDelete = listOfDependencies.Elements("Dependency")?.
          Where(p => p.Element("Id")?.Value == Convert.ToString(id)).FirstOrDefault();
        dependencyToDelete!.Remove();
        XMLTools.SaveListToXMLElement(listOfDependencies, XMLDEPENDENCY);
    }

    /// <summary>
    /// Reads Dependency object by his ID 
    /// </summary>
    /// <param name="id">id of object to read</param>
    /// <returns>the object in engineers DB with this id</returns>
    public Dependency? Read(int id)
    {
        const string XMLDEPENDENCY = @"dependencies";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        var elementToReturn = listOfDependencies.Elements("Dependency")?.
          Where(p => p.Element("Id")?.Value == Convert.ToString(id)).FirstOrDefault();
        if (elementToReturn is null) return null;
        int _id = Convert.ToInt16(elementToReturn!.Element("Id")!.Value);
        int _dependentTask = Convert.ToInt16(elementToReturn!.Element("DependentTask")!.Value);
        int _dependOnTask = Convert.ToInt16(elementToReturn!.Element("DependOnTask")!.Value);
        Dependency dependencyToReturn = new(_id, _dependentTask, _dependOnTask);
        return dependencyToReturn;
    }

    /// <summary>
    /// Reads a Dependency object by a bool function 
    /// </summary>
    /// <param name="filter">bool func to run each object</param>
    /// <returns>the first elment that return true to filter function</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        //const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies.xml";
        //XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        //var allDependencies = listOfDependencies.Elements("Dependency")?.
        //  Where(p => p.Element("Id")?.Value == Convert.ToString(id)).FirstOrDefault();
        //var engineerToReturn = allDependencies.Where(filter).ToList();
        //return (Dependency)engineerToReturn;
        return null;
    }

    /// <summary>
    /// gets an XElement item and returns this item as a Dependency item
    /// </summary>
    /// <param name="element">the item which accepted</param>
    /// <returns>a Dependency item</returns>
    private static Dependency? XElementToDependency(XElement element)
    {
        if (element is null)
            return null;
        Dependency dependency = new Dependency((int)(element.Element("Id")!), (int)(element.Element("DependentTask")!), (int)(element.Element("DependOnTask")!));
        return dependency;
    }

    /// <summary>
    /// Reads all Dependencies objects
    /// </summary>
    /// <returns>the whole list of the dependencies</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {

        XElement dependencies = XMLTools.LoadListFromXMLElement("dependencies");
        if (filter is null)
        {
            return dependencies.Elements("Dependency")
                .Select(dep => XElementToDependency(dep));
        }
        else
        {
            return dependencies.Elements("Dependency")
               .Select(dep => XElementToDependency(dep))
               .Where(filter);
        }
    }

    /// <summary>
    /// Updates Dependency object
    /// </summary>
    /// <param name="item">object item of dependcy to update</param>
    /// <exception cref="Exception">the input id of the dependency does not exist</exception>
    public void Update(Dependency item)
    {
        const string XMLDEPENDENCY = @"dependencies";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        var elementToUpdate = listOfDependencies.Elements("Dependency")?.
          Where(p => p.Element("Id")?.Value == Convert.ToString(item.Id)).FirstOrDefault();
        if (elementToUpdate is null) throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exist.");
        elementToUpdate.Remove();
        int _id = Convert.ToInt16(elementToUpdate!.Element("Id")!.Value);
        Dependency dependencyToReturn = new(_id, item.DependentTask, item.DependOnTask);
        XElement xelemntToAdd = new("Dependency");
        XElement _idxelemnt = new("Id");
        _idxelemnt.Value = Convert.ToString(_id);
        xelemntToAdd.Add(_idxelemnt);
        XElement _dependentTask = new("DependentTask");
        _dependentTask.Value = Convert.ToString(item.DependentTask);
        xelemntToAdd.Add(_dependentTask);
        XElement _dependOnTask = new("DependOnTask");
        _dependOnTask.Value = Convert.ToString(item.DependOnTask);
        xelemntToAdd.Add(_dependOnTask);
        listOfDependencies.Add(xelemntToAdd);
        XMLTools.SaveListToXMLElement(listOfDependencies, XMLDEPENDENCY);
    }
}
