namespace Dal;
using DalApi;
using DallXml;
using DO;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates new Task object in DAL
    /// </summary>
    /// <param name="item">item of task to create</param>
    /// <returns>the id of new object</returns>
    public int Create(Task item)
    {
        const string XMLTask = @"..\..\..\..\..\..\xml\tasks.xml";
        const string XMLCONFIG = @"..\..\..\..\..\..\xml\data-config.xml";
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>(XMLTask);
        List<string> listConfig = XMLTools.LoadListFromXMLSerializer<string>(XMLCONFIG);
        //add 1 to the next task id
        XMLTools.SaveListToXMLSerializer<Task>(list, XMLTask);
        listConfig[0]=Convert.ToString(Convert.ToInt16( listConfig[0]+1));
        XMLTools.SaveListToXMLSerializer<string>(listConfig, XMLCONFIG);
        //
        int newID = Config.NextTaskId;
        Task copy = item with { Id=newID };
        list.Add(copy);
        XMLTools.SaveListToXMLSerializer<Task>(list, XMLTask);
        return copy.Id;
    }
    /// <summary>
    /// Deletes a Task by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    public void Delete(int id)
    {
        const string XMLTask = @"..\..\..\..\..\..\xml\tasks.xml";
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>(XMLTask);
        Task? taskToDelete = Read(id);
        if (taskToDelete is null)
            throw new DalDoesNotExistException($"Task with ID = {id} does not exsist.");
        else
        {
            list.Remove(taskToDelete);
            XMLTools.SaveListToXMLSerializer<Task>(list,XMLTask);
        }
    }
    /// <summary>
    /// Reads entity task by his ID
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns>the item with this id</returns>
    public Task? Read(int id)
    {
        const string XMLTask = @"..\..\..\..\..\..\xml\tasks.xml";
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>(XMLTask);
        return list.Find(taskToReturn=> taskToReturn!.Id==id);
    }
    /// <summary>
    /// Reads entity task by a bool function
    /// </summary>
    /// <param name="filter">bool func to run each object</param>
    /// <returns>the first elment that return true to filter function</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        const string XMLTask = @"..\..\..\..\..\..\xml\tasks.xml";
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>(XMLTask);
        return list!.FirstOrDefault(filter);
    }
    /// <summary>
    /// Reads all task objects
    /// </summary>
    /// <returns>the whole list of the tasks</returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        const string XMLTask = @"..\..\..\..\..\..\xml\tasks.xml";
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>(XMLTask);
        if (filter == null)
            return list.Select(task => task);
        else return list!.Where<Task>(filter);
    }
    /// <summary>
    /// Updates a Task object
    /// </summary>
    /// <param name="item">object item of task to update</param>
    /// <exception cref="Exception">the input id of the task does not exist</exception>
    public void Update(Task item)
    {
        const string XMLTask = @"..\..\..\..\..\..\xml\tasks.xml";
        List<Task> list = XMLTools.LoadListFromXMLSerializer<Task>(XMLTask);
        Task? taskToUpdate = Read(item.Id)??throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist.");
        list.Remove(taskToUpdate);
        Task task = new(item.Id, item.Description, item.Alias, item.Milestone, item.CreatedAt, item.Start, item.ForecastDate, item.DeadLine, item.Complete, item.Deliverables, item.Remarks, item.EngineerId, item.ComplexilyLevel);
        list.Add(task);
        XMLTools.SaveListToXMLSerializer<Task>(list, XMLTask);
    }
}
