namespace DalApi;
using DO;

public interface IDependency
{
    int Create(Dependency item); //Creates new Dependency object in DAL
    Dependency? Read(int id); //Reads Dependency object by its ID 
    List<Dependency> ReadAll(); // Reads all Dependencies objects
    void Update(Dependency item); //Updates Dependency object
    void Delete(int id); //Deletes a Dependency by its Id
}
