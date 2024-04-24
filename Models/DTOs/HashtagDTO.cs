namespace NashGrub.Models.DTOs;

public class HashTagDTO
{
    public int Id { get; set; }
    public string BuisnessName { get; set; }

    public List<ReviewDTO> Reviews { get; set; }
}