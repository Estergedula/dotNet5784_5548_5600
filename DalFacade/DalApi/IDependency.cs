namespace DalApi;
using DO;
public interface IDependency:ICrud<Dependency>
{

}
//public interface IDependency : ICrud<Dependency>
//{
//    /// <summary>
//    /// Creates new Dependency object in DAL
//    /// </summary>
//    /// <param name="item">item to create in DB</param>
//    /// <returns>the id of the item</returns>
//    int Create(Dependency item);
//    /// <summary>
//    /// Reads Dependency object by its ID 
//    /// </summary>
//    /// <param name="id">id of object to read</param>
//    /// <returns></returns>
//    Dependency? Read(int id);
//    /// <summary>
//    /// Reads all Dependencies objects
//    /// </summary>
//    /// <returns>the whole list of the dependencies</returns>
//    IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null);
//    /// <summary>
//    /// Updates Dependency object
//    /// </summary>
//    /// <param name="item">object item of dependcy to update</param>
//    /// <exception cref="Exception">the input id of the dependency does not exist</exception>
//    void Update(Dependency item);
//    /// <summary>
//    /// Deletes a Dependency by its Id
//    /// </summary>
//    /// <param name="id">id of object to delete</param>
//    /// <exception cref="Exception">the param id is not exist in the DB</exception>
//    void Delete(int id); //Deletes a Dependency by its Id
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="filter"></param>
//    /// <returns></returns>
//    Dependency? Read(Func<Dependency, bool> filter);
//}
