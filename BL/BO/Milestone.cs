﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Milestone
{
    public int MileStoneId { get; init; }
    public string? Descriotion { get; init; }
    public string? Alias { get; init; }
    public IEnumerable<int>? Courses { get; init; } = null;
    public Status Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadLine { get; set; }
    public DateTime? Complete { get; set; }
    public double? CompletionPercentage { get; set; }
   public string? Remarks { get; set; }
   // public override string ToString() => this.ToStringProperty();
}
//○	מזהה
//○	תיאור
//○	כינוי
//○	רשימת המשימות השייכות לאבן דרך
//○	תאריך יצירה
//○	תאריך בייסליין
//○	תאריך התחלה
//○	סטטוס
//○	תאריך משוער לסיום
//○	תאריך אחרון לסיום
//○	תאריך סיום בפועל
//○	אחוז התקדמות
//○	הערות
