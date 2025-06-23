using Microsoft.EntityFrameworkCore;
using HouseholdManager.Data;
using HouseholdManager.Models;

namespace HouseholdManager.Services
{
    public class MealService
    {
        private readonly HouseholdContext _context;

        public MealService(HouseholdContext context)
        {
            _context = context;
        }

        public async Task<Meal> CreateMealAsync(int householdId, string name, string? description = null, 
            string? tags = null, List<MealIngredient>? ingredients = null)
        {
            var meal = new Meal
            {
                Name = name,
                Description = description,
                Tags = tags,
                HouseholdId = householdId
            };

            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            // Add ingredients if provided
            if (ingredients != null && ingredients.Any())
            {
                foreach (var ingredient in ingredients)
                {
                    ingredient.MealId = meal.Id;
                    _context.MealIngredients.Add(ingredient);
                }
                await _context.SaveChangesAsync();
            }

            return meal;
        }

        public async Task<Meal?> UpdateMealAsync(int mealId, string name, string? description = null, 
            string? tags = null, List<MealIngredient>? ingredients = null)
        {
            var meal = await _context.Meals
                .Include(m => m.MealIngredients)
                .FirstOrDefaultAsync(m => m.Id == mealId);
            
            if (meal != null)
            {
                meal.Name = name;
                meal.Description = description;
                meal.Tags = tags;

                // Update ingredients if provided
                if (ingredients != null)
                {
                    // Remove existing ingredients
                    _context.MealIngredients.RemoveRange(meal.MealIngredients);
                    
                    // Add new ingredients
                    foreach (var ingredient in ingredients)
                    {
                        ingredient.MealId = meal.Id;
                        _context.MealIngredients.Add(ingredient);
                    }
                }

                await _context.SaveChangesAsync();
            }
            return meal;
        }

        public async Task<List<Meal>> GetMealsByHouseholdAsync(int householdId)
        {
            return await _context.Meals
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .Where(m => m.HouseholdId == householdId)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<Meal?> GetMealWithIngredientsAsync(int mealId)
        {
            return await _context.Meals
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == mealId);
        }

        public async Task AddIngredientToMealAsync(int mealId, int ingredientId, 
            decimal? quantity = null, string? unit = null, string? notes = null, bool isOptional = false)
        {
            var mealIngredient = new MealIngredient
            {
                MealId = mealId,
                IngredientId = ingredientId,
                Quantity = quantity,
                Unit = unit,
                Notes = notes,
                IsOptional = isOptional
            };

            _context.MealIngredients.Add(mealIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveIngredientFromMealAsync(int mealId, int ingredientId)
        {
            var mealIngredient = await _context.MealIngredients
                .FirstOrDefaultAsync(mi => mi.MealId == mealId && mi.IngredientId == ingredientId);
            
            if (mealIngredient != null)
            {
                _context.MealIngredients.Remove(mealIngredient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Meal>> GetMealsByIngredientAsync(int householdId, int ingredientId)
        {
            return await _context.Meals
                .Include(m => m.MealIngredients)
                    .ThenInclude(mi => mi.Ingredient)
                .Where(m => m.HouseholdId == householdId && 
                           m.MealIngredients.Any(mi => mi.IngredientId == ingredientId))
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task MarkAsCookedAsync(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal != null)
            {
                meal.LastCookedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMealAsync(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkMealCookedAsync(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal != null)
            {
                meal.LastCookedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Meal>> GetRecentlyUnusedMealsAsync(int householdId, int daysThreshold = 30)
        {
            var cutoffDate = DateTime.Now.AddDays(-daysThreshold);
            return await _context.Meals
                .Where(m => m.HouseholdId == householdId && 
                           (m.LastCookedDate == null || m.LastCookedDate < cutoffDate))
                .OrderBy(m => m.LastCookedDate)
                .ToListAsync();
        }
    }
}