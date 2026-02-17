using System.ComponentModel.DataAnnotations;

namespace RuangApp.Api.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public required string roomName { get; set; }
    public required string location { get; set; }
    public required int capacity { get; set; }
    public bool isAvailable { get; set; }
}
