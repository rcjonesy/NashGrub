namespace NashGrub.Models.DTOs;

public class HashTagDTO
{
    public int Id { get; set; }
    public string? BusinessName { get; set; }

    public List<ReviewDTO>? Reviews { get; set; }
}