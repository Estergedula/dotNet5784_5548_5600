using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// A logical subinterface for a primary logical entity: Engineer
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer boEngineer);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool>? filter = null);
    public void Update(BO.Engineer item);
    public void Delete(int id);
}
