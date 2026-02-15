using System.ComponentModel.DataAnnotations;

namespace RuangApp.Api.Dtos;

public record Borrowing(
    int Id,
    [Required] int roomId,
    [Required] [StringLength(50)] string borrowerName,
    [Required] DateOnly borrowingDate,
    [Required] TimeOnly startTime,
    [Required] TimeOnly endTime,
    [Required] string purpose,
    [Required] string status
);
