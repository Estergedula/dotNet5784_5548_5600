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
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    private static BO.Status GetStatuesOfTask(DO.Task task)
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
        IEnumerable<BO.TaskInList> allTasks = from task in _dal.Task.ReadAll()
                                              select new BO.TaskInList
                                              {
                                                  Id = task.Id,
                                                  Description = task.Description!,
                                                  Alias = task!.Alias,
                                                  Status = GetStatuesOfTask(task),
                                              };
        return filter == null ? allTasks : allTasks.Where(filter);

    }
}
