using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// A logical subinterface for a logical auxiliary entity: Engineer-In-List
/// </summary>
public interface IEngineerInList
{
    public IEnumerable<BO.EngineerInList> ReadAll(Func<BO.EngineerInList?, bool>? filter = null);
}
