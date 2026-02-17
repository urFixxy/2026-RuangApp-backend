public class CreateBorrowingDto
{
    public int RoomId { get; set; }
    public DateOnly borrowingDate { get; set; }
    public TimeOnly startTime { get; set; }
    public TimeOnly endTime { get; set; }
    public string purpose { get; set; } = string.Empty;
}
