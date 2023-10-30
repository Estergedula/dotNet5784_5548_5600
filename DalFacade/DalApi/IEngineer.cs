namespace DalApi;
using DO;

public interface IEngineer
{
    int Create(Engineer item); //Creates new Engineer object in DAL
    Engineer? Read(int id); //Reads entity Engineer by its ID 
    List<Engineer> ReadAll(); //Reads all Engineers objects
    void Update(Engineer item); //Updates Engineer object
    void Delete(int id); //Deletes a Engineer by its Id
}
