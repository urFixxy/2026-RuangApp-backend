namespace RuangApp.Api.Dtos;

public record Borrowing(
    int Id,
    int idRoom,
    string borrowerName,
    DateOnly borrowingDate,
    TimeOnly startTime,
    TimeOnly endTime,
    string purpose,
    string status
);
