
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
        const string XMLENGINEER = @"..\..\..\..\..\..\xml\engineers.xml";
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement(XMLENGINEER);
        var engineerToReturn = listOfEngineers.Elements("Engineer")?.
          Where(p => p.Element("Id")?.Value == Convert.ToString(id)).FirstOrDefault();
        var bla = XMLTools.ToEnumNullable<Engineer>(engineerToReturn,
        return (Engineer)engineerToReturn;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        const string XMLENGINEER = @"..\..\..\..\..\..\xml\engineers.xml";
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement(XMLENGINEER);
        var allEngineer = from engineerToAdd in listOfEngineers.Elements("Address")
                              // where filter((string)engineerToAdd.to(!.Attribute("Type") == "Billing"
                          select engineerToAdd;
        allEngineer = allEngineer.ToList();
        var engineerToReturn = allEngineer.Where(filter).ToList();
        return (Engineer)engineerToReturn;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        const string XMLENGINEER = @"..\..\..\..\..\..\xml\engineers.xml";
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement(XMLENGINEER);
        return null;
    }

    public void Update(Dependency item)
    {
        const string XMLENGINEER = @"..\..\..\..\..\..\xml\engineers.xml";
        XElement listOfEngineers = XMLTools.LoadListFromXMLElement(XMLENGINEER);
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is null)
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exist.");
        //  listOfEngineers.Elements.Remove(item);
        Engineer engineer = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
        listOfEngineers.Add(engineer);
        XMLTools.SaveListToXMLElement(listOfEngineers, XMLENGINEER);
    }
}
