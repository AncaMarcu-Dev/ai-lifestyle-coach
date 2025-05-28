using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleCoach.Data;

[ApiController]
[Route("api/[controller]")]
public class JournalController : ControllerBase
{
    private readonly AppDbContext _context;

    public JournalController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEntries()
    {
        var entries = _context.JournalEntries.OrderByDescending(e => e.DateCreated).ToList();
        return Ok(entries);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry([FromBody] JournalEntry entry)
    {
        entry.DateCreated = DateTime.UtcNow;
        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync();
        return Ok(entry);
    }
}
