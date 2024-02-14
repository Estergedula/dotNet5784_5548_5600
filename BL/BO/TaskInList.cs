using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// task-on-list helper entity - for task list screen and milestones screen
/// </summary>
public class TaskInList
{
    public int Id { get; init; }
    public required string ?Description { get; set; }
    public string? Alias { get; set; }
    public Status Status { get; set; }
    public override string ToString() => this.ToStringProperty();


}
