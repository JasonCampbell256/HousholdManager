using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class GroceryList
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Foreign keys
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
        
        public int? AssociatedMealId { get; set; }
        public Meal? AssociatedMeal { get; set; }
        
        // Navigation properties
        public ICollection<GroceryItem> Items { get; set; } = new List<GroceryItem>();
    }
}