using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? Category { get; set; } // e.g., "Dairy", "Meat", "Vegetable", "Spice"
        
        [MaxLength(20)]
        public string? DefaultUnit { get; set; } // e.g., "cups", "lbs", "tsp", "pieces"
        
        [MaxLength(300)]
        public string? Notes { get; set; } // Storage tips, substitutions, etc.
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
        
        // Navigation properties
        public ICollection<MealIngredient> MealIngredients { get; set; } = new List<MealIngredient>();
    }
}