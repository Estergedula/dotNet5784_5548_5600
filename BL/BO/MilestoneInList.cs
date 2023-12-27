

namespace BO;

public class MilestoneInList
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public string? Alias { get; set; }
    public Status Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public double? CompletionPercentage { get; set; }
}
//}
//ישות עזר של אבני דרך-ברשימה - עבור מסך רשימת אבני דרך:
//תיאור
//כינוי
//תאריך יצירה
//סטטוס
//אחוז התקדמות
