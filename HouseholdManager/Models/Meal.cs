using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class Meal
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [MaxLength(200)]
        public string? Tags { get; set; } // Comma-separated tags
        
        public DateTime? LastCookedDate { get; set; }
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
        
        // Navigation properties
        public ICollection<MealIngredient> MealIngredients { get; set; } = new List<MealIngredient>();
    }
}