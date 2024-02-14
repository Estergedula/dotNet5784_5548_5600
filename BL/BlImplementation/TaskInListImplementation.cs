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

    /// <summary>
    /// Get the statues of a specifict task
    /// </summary>
    /// <param name="task">The DO task type entity for whom you want to receive its status</param>
    /// <returns>The status of the task who recieved</returns>
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

    /// <summary>
    /// Read all task-in-list entities who fulfill a certain condition for screening
    /// </summary>
    /// <param name="filter">The condition for screening</param>
    /// <returns>A set of the task-in-list entities who fulfill the condition</returns>
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
