using LifestyleCoach.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly AppDbContext _context;

    public GoalController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetGoals()
    {
        var goals = _context.Goals.ToList();
        return Ok(goals);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] Goal goal)
    {
        goal.CreatedAt = DateTime.UtcNow;
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();
        return Ok(goal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(int id, [FromBody] Goal goal)
    {
        var existing = _context.Goals.FirstOrDefault(g => g.Id == id);
        if (existing == null) return NotFound();

        existing.Title = goal.Title;
        existing.Description = goal.Description;
        existing.Completed = goal.Completed;

        await _context.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var goal = _context.Goals.FirstOrDefault(g => g.Id == id);
        if (goal == null) return NotFound();

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

