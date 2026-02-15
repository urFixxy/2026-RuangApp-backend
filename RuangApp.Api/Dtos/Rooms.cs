using System.ComponentModel.DataAnnotations;

namespace RuangApp.Api.Dtos;

public record Rooms(
    int Id,
    [Required] [StringLength(20)] string roomName,
    [Required] [StringLength(50)] string location,
    [Required] int capacity
);
