using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuangApp.Api.Data;
using RuangApp.Api.Models;

namespace RuangApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(RuangAppContext context) : ControllerBase
{
    private readonly RuangAppContext _context = context;

    // GET: api/rooms
    // Query parameters: search, capacity, isAvailable, location
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetRooms(
        [FromQuery] string? search,
        [FromQuery] int? capacity,
        [FromQuery] bool? isAvailable,
        [FromQuery] string? location)
    {
        var query = _context.Rooms.AsQueryable();

        // Search by roomName
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(r => r.roomName.Contains(search));
        }

        // Filter by capacity
        if (capacity.HasValue)
        {
            query = query.Where(r => r.capacity >= capacity.Value);
        }

        // Filter by isAvailable
        if (isAvailable.HasValue)
        {
            query = query.Where(r => r.isAvailable == isAvailable.Value);
        }

        // Filter by location
        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(r => r.location.Contains(location));
        }

        var rooms = await query.ToListAsync();
        return Ok(rooms);
    }

    // GET: api/rooms/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> GetRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            return NotFound(new { message = "Room tidak ditemukan" });
        return Ok(room);
    }

    // POST: api/rooms
    [HttpPost]
    public async Task<ActionResult<Room>> PostRoom(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    // PUT: api/rooms/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(int id, Room room)
    {
        if (id != room.Id)
            return BadRequest(new { message = "ID tidak sesuai" });

        _context.Entry(room).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoomExists(id))
                return NotFound(new { message = "Room tidak ditemukan" });
            throw;
        }
        return NoContent();
    }

    // DELETE: api/rooms/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            return NotFound(new { message = "Room tidak ditemukan" });

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool RoomExists(int id)
    {
        return _context.Rooms.Any(e => e.Id == id);
    }
}
