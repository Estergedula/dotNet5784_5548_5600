namespace BO;
public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public MilestomeInList? Milestone { get; set; }
    public Status Status { get; set; }
    public IEnumerable<TaskInList>? DependenciesList { get; set; }
    public DateTime CreatedAt { get; set; }//תאריך יצירה
    public DateTime BaselineStartDate { get;  set; }//תאריך התחלה משוער
    public DateTime Start { get; set; }//תאריך התחלה בפועל
    public DateTime ForecastDate { get; set; }//תאריך משוער לסיום
    public DateTime DeadLine { get; set; }//תאריך אחרון לסיום
    public DateTime Complete { get; set; }//תאריך סיום בפועל
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperience ComplexilyLevel { get; set; }
    public DateTime RegistrationDate { get; init; }

}
//    int Id,
//    string? Description,
//    string? Alias,
//    bool Milestone,
//    DateTime CreatedAt,
//    DateTime Start,
//    DateTime ForecastDate,
//    DateTime DeadLine,
//    DateTime Complete,
//    string? Deliverables = null,
//    string? Remarks = null,
//    int EngineerId = 0,
//    EngineerExperience ComplexilyLevel = EngineerExperience.Junior,
//    bool isActive = true

//○	מזהה 
//○	תיאור
//○	כינוי
//○	אם קיים, מזהה אבן-דרך שהמשימה שייכת
//○	תאריך יצירה
//○	תאריך בייסליין
//○	תאריך התחלה בפועל
//○	סטטוס
//○	תאריך משוער לסיום
//○	תאריך אחרון לסיום
//○	תאריך סיום בפועל
//○	תוצר(מחרוזת המתארת את התוצר)
//○	הערות
//○	אם קיים, מזהה ושם המהנדס שהוקצה למשימה
//○	אבן דרך קשורה(מזהה וכינוי)
//○	רמת קושי
