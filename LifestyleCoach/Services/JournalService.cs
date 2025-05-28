using LifestyleCoach.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LifestyleCoach.Services
{
    public class JournalService
    {
        private readonly AppDbContext _dbContext;

        public JournalService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<JournalEntry>> GetAllEntriesAsync()
        {
            return await _dbContext.JournalEntries.AsNoTracking().ToListAsync();
        }

        public async Task<JournalEntry> GetEntryByIdAsync(int id)
        {
            return await _dbContext.JournalEntries.FindAsync(id);
        }

        public async Task AddEntryAsync(JournalEntry entry)
        {
            _dbContext.JournalEntries.Add(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEntryAsync(JournalEntry entry)
        {
            _dbContext.JournalEntries.Update(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntryAsync(int id)
        {
            var entry = await _dbContext.JournalEntries.FindAsync(id);
            if (entry != null)
            {
                _dbContext.JournalEntries.Remove(entry);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
