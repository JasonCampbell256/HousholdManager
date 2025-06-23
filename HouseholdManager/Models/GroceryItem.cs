using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? Category { get; set; }
        
        public decimal Quantity { get; set; } = 1;
        
        [MaxLength(20)]
        public string? Unit { get; set; } // e.g., "lbs", "pieces", "gallons"
        
        public bool IsChecked { get; set; }
        
        // Foreign key
        public int GroceryListId { get; set; }
        public GroceryList GroceryList { get; set; } = null!;
    }
}