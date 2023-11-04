﻿using Dal;
using DalApi;
namespace DalTest;

sealed public class DalList : IDal
{
    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();
}
