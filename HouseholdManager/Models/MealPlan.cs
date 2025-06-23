using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public DateTime WeekStartDate { get; set; }
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
        
        // Navigation properties
        public ICollection<MealPlanEntry> Entries { get; set; } = new List<MealPlanEntry>();
    }
}