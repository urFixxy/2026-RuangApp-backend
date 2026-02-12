using System;

namespace RuangApp.Api.Models;

public class Rooms
{
    public int Id { get; set; }
    public required string roomName { get; set; }
    public required string location { get; set; }
    public int capacity { get; set; }
}
