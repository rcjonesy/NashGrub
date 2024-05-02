namespace NashGrub.Models;

public class Review
{
    public int Id { get; set; }
    public string? Message { get; set; }
    public DateTime DateCreated { get; set; }
    public int HashtagId { get; set; }
    public Hashtag? Hashtag { get; set; }
    public int DaysSincePosted
    {
        get
        {
            TimeSpan timeDifference = DateTime.Now - DateCreated;
            return (int)timeDifference.TotalDays;
        }
    }
}