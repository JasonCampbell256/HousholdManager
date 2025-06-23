using Microsoft.EntityFrameworkCore;
using HouseholdManager.Data;
using HouseholdManager.Models;

namespace HouseholdManager.Services
{
    public class HouseholdService
    {
        private readonly HouseholdContext _context;

        public HouseholdService(HouseholdContext context)
        {
            _context = context;
        }

        public async Task<Household> CreateHouseholdAsync(string name)
        {
            var household = new Household
            {
                Name = name,
                CreatedDate = DateTime.Now
            };

            _context.Households.Add(household);
            await _context.SaveChangesAsync();
            return household;
        }

        public async Task<List<Household>> GetAllHouseholdsAsync()
        {
            return await _context.Households
                .Include(h => h.Members)
                .ToListAsync();
        }

        public async Task<Household?> GetHouseholdByIdAsync(int id)
        {
            return await _context.Households
                .Include(h => h.Members)
                .Include(h => h.Meals)
                .Include(h => h.Chores)
                .Include(h => h.MaintenanceTasks)
                .Include(h => h.Notes)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}