using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public record Engineer
(
    int Id,
    string Name,
    string? Alias = null,
    bool IsActive = false,
    Year CurrentYear = Year.FirstYear,
     DateTime? BirthDate = null
);
{
}

{
    /// <summary>
    /// Date - creation date of the current student record
    /// </summary>
    public DateTime Date => DateTime.Now; //get only
}
