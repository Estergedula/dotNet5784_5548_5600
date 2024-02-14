using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// A logical subinterface for a logical auxiliary entity: Task-In-List
/// </summary>
public interface ITaskInList
{
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList?, bool>? filter = null);

}
