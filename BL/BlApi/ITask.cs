﻿namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool> filter);
    public void Update(BO.Task boTask);
    public void Delete(int id);
}
