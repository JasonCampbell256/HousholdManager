using Microsoft.EntityFrameworkCore;
using HouseholdManager.Data;
using HouseholdManager.Models;

namespace HouseholdManager.Services
{
    public class MealPlanService
    {
        private readonly HouseholdContext _context;

        public MealPlanService(HouseholdContext context)
        {
            _context = context;
        }

        public async Task<MealPlan> CreateOrGetMealPlanAsync(int householdId, DateTime weekStartDate)
        {
            // Ensure the date is set to the start of the week (Sunday)
            var startOfWeek = GetStartOfWeek(weekStartDate);
            
            var existingPlan = await _context.MealPlans
                .Include(mp => mp.Entries)
                    .ThenInclude(e => e.Meal)
                .FirstOrDefaultAsync(mp => mp.HouseholdId == householdId && mp.WeekStartDate.Date == startOfWeek.Date);

            if (existingPlan != null)
            {
                return existingPlan;
            }

            // Create new meal plan
            var mealPlan = new MealPlan
            {
                Name = $"Week of {startOfWeek:MMM dd, yyyy}",
                WeekStartDate = startOfWeek,
                HouseholdId = householdId
            };

            _context.MealPlans.Add(mealPlan);
            await _context.SaveChangesAsync();

            // Create empty entries for the week
            await CreateWeeklyEntriesAsync(mealPlan.Id, startOfWeek);

            // Return the meal plan with entries
            return await GetMealPlanWithEntriesAsync(mealPlan.Id) ?? mealPlan;
        }

        public async Task<MealPlan?> GetMealPlanWithEntriesAsync(int mealPlanId)
        {
            return await _context.MealPlans
                .Include(mp => mp.Entries)
                    .ThenInclude(e => e.Meal)
                .FirstOrDefaultAsync(mp => mp.Id == mealPlanId);
        }

        public async Task<List<MealPlan>> GetMealPlansByHouseholdAsync(int householdId, int weeksToRetrieve = 4)
        {
            var cutoffDate = DateTime.Now.AddDays(-7 * weeksToRetrieve);
            
            return await _context.MealPlans
                .Include(mp => mp.Entries)
                    .ThenInclude(e => e.Meal)
                .Where(mp => mp.HouseholdId == householdId && mp.WeekStartDate >= cutoffDate)
                .OrderByDescending(mp => mp.WeekStartDate)
                .ToListAsync();
        }

        public async Task<MealPlanEntry?> UpdateMealPlanEntryAsync(int entryId, int? mealId, string? customMealName = null)
        {
            var entry = await _context.MealPlanEntries.FindAsync(entryId);
            if (entry == null) return null;

            entry.MealId = mealId;
            entry.CustomMealName = customMealName;

            await _context.SaveChangesAsync();

            return await _context.MealPlanEntries
                .Include(e => e.Meal)
                .FirstOrDefaultAsync(e => e.Id == entryId);
        }

        public async Task<bool> ClearMealPlanEntryAsync(int entryId)
        {
            var entry = await _context.MealPlanEntries.FindAsync(entryId);
            if (entry == null) return false;

            entry.MealId = null;
            entry.CustomMealName = null;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MealPlanEntry>> GetEntriesForWeekAsync(int householdId, DateTime weekStartDate)
        {
            var startOfWeek = GetStartOfWeek(weekStartDate);
            var endOfWeek = startOfWeek.AddDays(6);

            return await _context.MealPlanEntries
                .Include(e => e.Meal)
                .Include(e => e.MealPlan)
                .Where(e => e.MealPlan.HouseholdId == householdId && 
                           e.Date.Date >= startOfWeek.Date && 
                           e.Date.Date <= endOfWeek.Date)
                .OrderBy(e => e.Date)
                .ThenBy(e => e.MealTime)
                .ToListAsync();
        }

        public async Task<MealPlan?> GetCurrentWeekPlanAsync(int householdId)
        {
            var currentWeekStart = GetStartOfWeek(DateTime.Now);
            return await CreateOrGetMealPlanAsync(householdId, currentWeekStart);
        }

        public async Task<MealPlan?> GetPreviousWeekPlanAsync(int householdId)
        {
            var previousWeekStart = GetStartOfWeek(DateTime.Now).AddDays(-7);
            return await CreateOrGetMealPlanAsync(householdId, previousWeekStart);
        }

        private async Task CreateWeeklyEntriesAsync(int mealPlanId, DateTime weekStartDate)
        {
            var entries = new List<MealPlanEntry>();

            for (int day = 0; day < 7; day++)
            {
                var currentDate = weekStartDate.AddDays(day);
                
                foreach (MealTime mealTime in Enum.GetValues<MealTime>())
                {
                    entries.Add(new MealPlanEntry
                    {
                        MealPlanId = mealPlanId,
                        Date = currentDate,
                        MealTime = mealTime
                    });
                }
            }

            _context.MealPlanEntries.AddRange(entries);
            await _context.SaveChangesAsync();
        }

        private static DateTime GetStartOfWeek(DateTime date)
        {
            // Get Sunday as start of week
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public async Task<bool> DeleteMealPlanAsync(int mealPlanId)
        {
            var mealPlan = await _context.MealPlans.FindAsync(mealPlanId);
            if (mealPlan == null) return false;

            _context.MealPlans.Remove(mealPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Meal>> GetMealSuggestionsAsync(int householdId, string searchTerm = "")
        {
            var query = _context.Meals.Where(m => m.HouseholdId == householdId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(m => m.Name.Contains(searchTerm) || 
                                       (m.Tags != null && m.Tags.Contains(searchTerm)));
            }

            return await query
                .OrderBy(m => m.Name)
                .Take(10)
                .ToListAsync();
        }
    }
}