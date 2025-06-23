using Microsoft.EntityFrameworkCore;
using HouseholdManager.Data;
using HouseholdManager.Models;

namespace HouseholdManager.Services
{
    public class IngredientService
    {
        private readonly HouseholdContext _context;

        public IngredientService(HouseholdContext context)
        {
            _context = context;
        }

        public async Task<Ingredient> CreateIngredientAsync(int householdId, string name, 
            string? category = null, string? defaultUnit = null, string? notes = null)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                Category = category,
                DefaultUnit = defaultUnit,
                Notes = notes,
                HouseholdId = householdId
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<List<Ingredient>> GetIngredientsByHouseholdAsync(int householdId)
        {
            return await _context.Ingredients
                .Where(i => i.HouseholdId == householdId)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<List<Ingredient>> SearchIngredientsAsync(int householdId, string searchTerm)
        {
            return await _context.Ingredients
                .Where(i => i.HouseholdId == householdId && 
                           i.Name.Contains(searchTerm))
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<Ingredient?> GetIngredientByNameAsync(int householdId, string name)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.HouseholdId == householdId && 
                                         i.Name.ToLower() == name.ToLower());
        }

        public async Task<Ingredient?> UpdateIngredientAsync(int ingredientId, string name, 
            string? category = null, string? defaultUnit = null, string? notes = null)
        {
            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (ingredient != null)
            {
                ingredient.Name = name;
                ingredient.Category = category;
                ingredient.DefaultUnit = defaultUnit;
                ingredient.Notes = notes;
                await _context.SaveChangesAsync();
            }
            return ingredient;
        }

        public async Task DeleteIngredientAsync(int ingredientId)
        {
            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetCategoriesAsync(int householdId)
        {
            return await _context.Ingredients
                .Where(i => i.HouseholdId == householdId && !string.IsNullOrEmpty(i.Category))
                .Select(i => i.Category!)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }
    }
}