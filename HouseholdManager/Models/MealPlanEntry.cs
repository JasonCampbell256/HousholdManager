using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public enum MealTime
    {
        Breakfast,
        Lunch,
        Dinner
    }

    public class MealPlanEntry
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public MealTime MealTime { get; set; }
        
        // Foreign keys
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; } = null!;
        
        public int? MealId { get; set; }
        public Meal? Meal { get; set; }
        
        [MaxLength(100)]
        public string? CustomMealName { get; set; } // For meals not in the Meal table
    }
}