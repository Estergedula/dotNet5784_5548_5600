using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level"></param>
/// <param name="cost"></param>
public record Engineer
(
    int Id,
    string Name,
    string? Email = null,
    EngineerExperience Level = EngineerExperience.Junior,
    double? cost = 0
);
{
}

{
    /// <summary>
    /// Date - creation date of the current student record
    /// </summary>
    public DateTime Date => DateTime.Now; //get only
}
