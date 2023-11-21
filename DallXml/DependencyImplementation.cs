
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Resolvers;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies.xml";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"dependency with ID={item.Id} already exists");
        listOfDependencies.Add(item);
        XMLTools.SaveListToXMLElement(listOfDependencies, XMLDEPENDENCY);
        return item.Id;
    }

    public void Delete(int id)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies.xml";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist.");
        XElement? engineerToDelete = listOfDependencies.Elements("Dependency")?.
          Where(p => p.Element("Id")?.Value == Convert.ToString(id)).FirstOrDefault();
        engineerToDelete!.Remove();
        engineerToDelete.Save(XMLDEPENDENCY);
    }

    public Dependency? Read(int id)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies.xml";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        var dependencyToReturn = listOfDependencies.Elements("Dependency")?.
          Where(p => p.Element("Id")?.Value == Convert.ToString(id)).FirstOrDefault();
        var bla = XMLTools.ToEnumNullable!<Dependency>(dependencyToReturn, XMLDEPENDENCY);
        return (Dependency)dependencyToReturn;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies.xml";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        var allDependencies = from engineerToAdd in listOfDependencies.Elements("Address")
                              // where filter((string)engineerToAdd.to(!.Attribute("Type") == "Billing"
                          select engineerToAdd;
        allDependencies = allDependencies.ToList();
        var engineerToReturn = allDependencies.Where(filter).ToList();
        return (Dependency)engineerToReturn;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies.xml";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        return null;
    }

    public void Update(Dependency item)
    {
        const string XMLDEPENDENCY = @"..\..\..\..\..\..\xml\dependencies.xml";
        XElement listOfDependencies = XMLTools.LoadListFromXMLElement(XMLDEPENDENCY);
        Dependency? dependencyToUpdate = Read(item.Id);
        if (dependencyToUpdate is null)
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exist.");
        //  listOfEngineers.Elements.Remove(item);
        Dependency dependency = new(item.Id, item.DependentTask, item.DependOnTask);
        listOfDependencies.Add(dependency);
        XMLTools.SaveListToXMLElement(listOfDependencies, XMLDEPENDENCY);
    }
}
