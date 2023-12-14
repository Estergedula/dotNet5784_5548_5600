using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public required string ?Description { get; set; }
    public string? Alias { get; set; }
    public Status Status { get; set; }
 
}
//●	ישות עזר של משימה-ברשימה - עבור מסך רשימת משימות ומסך אבני דרך:
//○	מזהה 
//○	תיאור
//○	כינוי
//○	סטטוס

