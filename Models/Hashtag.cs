namespace NashGrub.Models;

public class Hashtag
{
    public int Id { get; set; }
    public string BusinessName { get; set; }

    public List<Review> Reviews { get; set; }
}