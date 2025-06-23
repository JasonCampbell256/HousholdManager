using Microsoft.EntityFrameworkCore;
using HouseholdManager.Data;
using HouseholdManager.Models;

namespace HouseholdManager.Services
{
    public class MaintenanceService
    {
        private readonly HouseholdContext _context;

        public MaintenanceService(HouseholdContext context)
        {
            _context = context;
        }

        public async Task<MaintenanceTask> CreateMaintenanceTaskAsync(int householdId, string name, 
            string? description = null, ChoreFrequency frequency = ChoreFrequency.Monthly)
        {
            var task = new MaintenanceTask
            {
                Name = name,
                Description = description,
                Frequency = frequency,
                HouseholdId = householdId,
                NextDueDate = CalculateNextDueDate(frequency)
            };

            _context.MaintenanceTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<MaintenanceTask>> GetMaintenanceTasksByHouseholdAsync(int householdId)
        {
            return await _context.MaintenanceTasks
                .Where(mt => mt.HouseholdId == householdId)
                .OrderBy(mt => mt.NextDueDate)
                .ToListAsync();
        }

        public async Task<List<MaintenanceTask>> GetOverdueMaintenanceTasksAsync(int householdId)
        {
            return await _context.MaintenanceTasks
                .Where(mt => mt.HouseholdId == householdId && mt.NextDueDate <= DateTime.Now)
                .OrderBy(mt => mt.NextDueDate)
                .ToListAsync();
        }

        public async Task MarkMaintenanceTaskCompletedAsync(int taskId)
        {
            var task = await _context.MaintenanceTasks.FindAsync(taskId);
            if (task != null)
            {
                task.LastCompletedDate = DateTime.Now;
                task.NextDueDate = CalculateNextDueDate(task.Frequency, DateTime.Now);
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