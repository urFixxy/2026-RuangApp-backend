namespace RuangApp.Api.Dtos;

public record Rooms(
    int Id,
    string roomName,
    string location,
    int capacity
);
