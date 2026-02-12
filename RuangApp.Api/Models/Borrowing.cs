using System;

namespace RuangApp.Api.Models;

public class Borrowing
{
    public int Id { get; set; }
    public required string borrowerName { get; set; }
    public int RoomId { get; set; }
    public Rooms? Room { get; set; }
    public DateOnly borrowingDate { get; set; }
    public TimeOnly startTime { get; set; }
    public TimeOnly endTime { get; set; }
    public required string purpose { get; set; }
    public required string status { get; set; }
}
