namespace BlApi;

public interface IMilestone
{
    //public int Create(BO.Milestone item);
    public BO.Milestone CreateLUZ(int id);
    public BO.Engineer? Read(int id);
    //public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool> filter);
    public void Update(int id);
   // public void Delete(int id);
}
