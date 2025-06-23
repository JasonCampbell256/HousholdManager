using Microsoft.EntityFrameworkCore;
using HouseholdManager.Data;
using HouseholdManager.Models;

namespace HouseholdManager.Services
{
    public class ChoreService
    {
        private readonly HouseholdContext _context;

        public ChoreService(HouseholdContext context)
        {
            _context = context;
        }

        public async Task<Chore> CreateChoreAsync(int householdId, string name, string? description = null, 
            ChoreFrequency frequency = ChoreFrequency.Weekly, int? assignedToId = null)
        {
            var chore = new Chore
            {
                Name = name,
                Description = description,
                Frequency = frequency,
                HouseholdId = householdId,
                AssignedToId = assignedToId,
                NextDueDate = CalculateNextDueDate(frequency)
            };

            _context.Chores.Add(chore);
            await _context.SaveChangesAsync();
            return chore;
        }

        public async Task<List<Chore>> GetChoresByHouseholdAsync(int householdId)
        {
            return await _context.Chores
                .Include(c => c.AssignedTo)
                .Where(c => c.HouseholdId == householdId)
                .OrderBy(c => c.NextDueDate)
                .ToListAsync();
        }

        public async Task<List<Chore>> GetOverdueChoresAsync(int householdId)
        {
            return await _context.Chores
                .Include(c => c.AssignedTo)
                .Where(c => c.HouseholdId == householdId && 
                           c.NextDueDate <= DateTime.Now && 
                           !c.IsCompleted)
                .OrderBy(c => c.NextDueDate)
                .ToListAsync();
        }

        public async Task CompleteChoreAsync(int choreId)
        {
            var chore = await _context.Chores.FindAsync(choreId);
            if (chore != null)
            {
                chore.IsCompleted = true;
                chore.LastCompletedDate = DateTime.Now;
                chore.NextDueDate = CalculateNextDueDate(chore.Frequency, DateTime.Now);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteChoreAsync(int choreId)
        {
            var chore = await _context.Chores.FindAsync(choreId);
            if (chore != null)
            {
                _context.Chores.Remove(chore);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkChoreCompletedAsync(int choreId)
        {
            var chore = await _context.Chores.FindAsync(choreId);
            if (chore != null)
            {
                chore.IsCompleted = true;
                chore.LastCompletedDate = DateTime.Now;
                chore.NextDueDate = CalculateNextDueDate(chore.Frequency, DateTime.Now);
                await _context.SaveChangesAsync();
            }
        }

        private DateTime? CalculateNextDueDate(ChoreFrequency frequency, DateTime? fromDate = null)
        {
            var baseDate = fromDate ?? DateTime.Now;
            
            return frequency switch
            {
                ChoreFrequency.Daily => baseDate.AddDays(1),
                ChoreFrequency.Weekly => baseDate.AddDays(7),
                ChoreFrequency.Monthly => baseDate.AddMonths(1),
                ChoreFrequency.AsNeeded => null,
                _ => null
            };
        }
    }
}