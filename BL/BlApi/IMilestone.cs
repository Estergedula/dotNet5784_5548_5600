namespace BlApi;

public interface IMilestone
{
    //public int Create(BO.Milestone item);
    public BO.Milestone CreateLUZ();
    public BO.Milestone? Read(int id);
    //public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool> filter);
    public BO.Milestone Update(BO.Task task);
   // public void Delete(int id);
}
