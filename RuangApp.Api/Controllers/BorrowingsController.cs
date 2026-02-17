using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuangApp.Api.Data;
using RuangApp.Api.Dtos;
using RuangApp.Api.Models;

namespace RuangApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BorrowingsController(RuangAppContext context) : ControllerBase
{
    private readonly RuangAppContext _context = context;

    // GET: api/borrowings
    // Query parameters: search, status, roomId, borrowingDate
    // Only Admin can view all borrowings
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<IEnumerable<Borrowing>>> GetBorrowings(
        [FromQuery] string? search,
        [FromQuery] string? status,
        [FromQuery] int? roomId,
        [FromQuery] string? borrowingDate)
    {
        var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var username = User.Identity?.Name;

        var query = _context.Borrowings
            .Include(b => b.Room)
            .AsQueryable();

        // Jika User biasa, hanya lihat data miliknya
        if (userRole == "User")
        {
            query = query.Where(b => b.borrowerName == username);
        }

        // Admin bisa pakai filter
        if (!string.IsNullOrEmpty(search))
            query = query.Where(b => b.borrowerName.Contains(search));

        if (!string.IsNullOrEmpty(status))
            query = query.Where(b => b.status == status);

        if (roomId.HasValue)
            query = query.Where(b => b.RoomId == roomId.Value);

        if (!string.IsNullOrEmpty(borrowingDate) && DateOnly.TryParse(borrowingDate, out var date))
            query = query.Where(b => b.borrowingDate == date);

        return Ok(await query.ToListAsync());
    }

    // GET: api/borrowings/5
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Borrowing>> GetBorrowing(int id)
    {
        var borrowing = await _context.Borrowings.Include(b => b.Room).FirstOrDefaultAsync(b => b.Id == id);
        if (borrowing == null)
            return NotFound(new { message = "Borrowing tidak ditemukan" });
        return Ok(borrowing);
    }

    // POST: api/borrowings
    // User dapat membuat peminjaman, Admin juga dapat
    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult<Borrowing>> PostBorrowing(CreateBorrowingDto dto)
    {
        var borrowing = new Borrowing
        {
            RoomId = dto.RoomId,
            borrowingDate = dto.borrowingDate,
            startTime = dto.startTime,
            endTime = dto.endTime,
            purpose = dto.purpose,
            borrowerName = User.Identity!.Name!,
            status = "Pending"
        };

        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBorrowing), new { id = borrowing.Id }, borrowing);
    }


    // PUT: api/borrowings/5
    // Hanya Admin yang dapat mengubah
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutBorrowing(int id, Borrowing borrowing)
    {
        if (id != borrowing.Id)
            return BadRequest(new { message = "ID tidak sesuai" });

        _context.Entry(borrowing).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(id))
                return NotFound(new { message = "Borrowing tidak ditemukan" });
            throw;
        }
        return NoContent();
    }

    // DELETE: api/borrowings/5
    // Hanya Admin yang dapat menghapus
    [HttpDelete("{id}")]
    [Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> DeleteBorrowing(int id)
    {
        var borrowing = await _context.Borrowings.FindAsync(id);
        if (borrowing == null)
            return NotFound(new { message = "Borrowing tidak ditemukan" });

        var username = User.Identity?.Name;
        var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        if (userRole != "Admin" && borrowing.borrowerName != username)
            return Forbid();

        _context.Borrowings.Remove(borrowing);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool BorrowingExists(int id)
    {
        return _context.Borrowings.Any(e => e.Id == id);
    }
}
