namespace DalApi;
using DO;

public interface IEngineer:ICrud<Engineer>
{
    /// <summary>
    /// Creates new Engineer object in DAL
    /// </summary>
    /// <param name="item">item of enginner to create</param>
    /// <returns></returns>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    int Create(Engineer item); 
    /// <summary>
    /// Reads entity Engineer by its ID
    /// </summary>
    /// <param name="id">id of the engineer to read</param>
    /// <returns></returns>
    Engineer? Read(int id);
    /// <summary>
    /// Reads all engineers objects
    /// </summary>
    /// <returns>the whole list of the engineers</returns>
    IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null);
    /// <summary>
    /// Updates Engineer object
    /// </summary>
    /// <param name="item">object item of engineer to update</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    void Update(Engineer item);
    /// <summary>
    /// Deletes a Engineer by its Id
    /// </summary>
    /// <param name="id">id of object to delete</param>
    /// <exception cref="Exception">the input id of the engineer does not exist</exception>
    void Delete(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Engineer? Read(Func<Engineer, bool> filter);
}
