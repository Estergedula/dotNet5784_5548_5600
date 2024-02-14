using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Engineer Entity represents an engineer with all its props.
/// </summary>
/// <param name="Id">Personal unique ID of engineer (as in national id card)</param>
/// <param name="Name">Name of the engineer</param>
/// <param name="Email">Email address of the engineer</param>
/// <param name="Level">The level of the engineer</param>
/// <param name="Cost">Hourly cost of the engineer</param>
/// <param name="CurrentTask">The task the engineer is working on currently</param>
public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerExperience Level { get; set; }
    public double? Cost { get; set; }
    public TaskInEngineer? CurrentTask { get; set; }
    public override string ToString() => this.ToStringProperty();
}
