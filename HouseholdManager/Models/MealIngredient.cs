using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class MealIngredient
    {
        public int Id { get; set; }
        
        // Foreign keys
        public int MealId { get; set; }
        public Meal Meal { get; set; } = null!;
        
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        
        // Quantity information
        public decimal? Quantity { get; set; }
        
        [MaxLength(20)]
        public string? Unit { get; set; } // e.g., "cups", "lbs", "tsp", "pieces"
        
        [MaxLength(200)]
        public string? Notes { get; set; } // e.g., "chopped", "optional", "to taste"
        
        public bool IsOptional { get; set; } = false;
    }
}