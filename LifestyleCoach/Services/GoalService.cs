using LifestyleCoach.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifestyleCoach.Services
{
    public class GoalService
    {
        private readonly AppDbContext _dbContext;

        public GoalService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Goal>> GetAllGoalsAsync()
        {
            return await _dbContext.Goals.AsNoTracking().ToListAsync();
        }

        public async Task<Goal> GetGoalByIdAsync(int id)
        {
            return await _dbContext.Goals.FindAsync(id);
        }

        public async Task AddGoalAsync(Goal goal)
        {
            _dbContext.Goals.Add(goal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateGoalAsync(Goal goal)
        {
            _dbContext.Goals.Update(goal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGoalAsync(int id)
        {
            var goal = await _dbContext.Goals.FindAsync(id);
            if (goal != null)
            {
                _dbContext.Goals.Remove(goal);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
