using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuangApp.Api.Data;
using RuangApp.Api.Models;

namespace RuangApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowingsController(RuangAppContext context) : ControllerBase
{
    private readonly RuangAppContext _context = context;

    // GET: api/borrowings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Borrowing>>> GetBorrowings()
    {
        var borrowings = await _context.Borrowings.Include(b => b.Room).ToListAsync();
        return Ok(borrowings);
    }

    // GET: api/borrowings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Borrowing>> GetBorrowing(int id)
    {
        var borrowing = await _context.Borrowings.Include(b => b.Room).FirstOrDefaultAsync(b => b.Id == id);
        if (borrowing == null)
            return NotFound(new { message = "Borrowing tidak ditemukan" });
        return Ok(borrowing);
    }

    // POST: api/borrowings
    [HttpPost]
    public async Task<ActionResult<Borrowing>> PostBorrowing(Borrowing borrowing)
    {
        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBorrowing), new { id = borrowing.Id }, borrowing);
    }

    // PUT: api/borrowings/5
    [HttpPut("{id}")]
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrowing(int id)
    {
        var borrowing = await _context.Borrowings.FindAsync(id);
        if (borrowing == null)
            return NotFound(new { message = "Borrowing tidak ditemukan" });

        _context.Borrowings.Remove(borrowing);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool BorrowingExists(int id)
    {
        return _context.Borrowings.Any(e => e.Id == id);
    }
}
