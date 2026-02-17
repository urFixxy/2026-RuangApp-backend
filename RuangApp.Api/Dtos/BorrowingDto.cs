using System.ComponentModel.DataAnnotations;

namespace RuangApp.Api.Dtos;

public class BorrowingDto
{
    public int Id { get; set; }
    public required int RoomId { get; set; }
    public required string RoomName { get; set; }
    public required string BorrowerName { get; set; }
    public required DateOnly BorrowingDate { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public required string Purpose { get; set; }
    public required string Status { get; set; }
}
