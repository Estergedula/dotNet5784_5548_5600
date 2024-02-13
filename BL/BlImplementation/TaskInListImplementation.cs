using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class TaskInListImplementation : ITaskInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BO.Status getStatuesOfTask(DO.Task task)
    {
        DateTime now = DateTime.Now;
        if (task.ScheduleDate==null)
            return BO.Status.Unscheduled;
        else if (task.Start == null)
            return BO.Status.Scheduled;
        else if (task.DeadLine < now && task.Complete == null)
            return BO.Status.InJeopardy;
        else return BO.Status.OnTrack;

    }
    public IEnumerable<TaskInList> ReadAll(Func<TaskInList?, bool>? filter = null)
    {
        IEnumerable<DO.Task?> allTasks = _dal.Task.ReadAll((Func<DO.Task?, bool>?)filter);
        IEnumerable<BO.TaskInList> allTaskinBo = allTasks.Select(task => new BO.TaskInList
        {
            Id = task!.Id,
            Description = task!.Description!,
            Alias = task!.Alias,
            Status=getStatuesOfTask(task)   
        });
        return allTaskinBo;
    }
}
