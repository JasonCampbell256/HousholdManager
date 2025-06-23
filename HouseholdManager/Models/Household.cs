using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class Household
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public ICollection<FamilyMember> Members { get; set; } = new List<FamilyMember>();
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
        public ICollection<Chore> Chores { get; set; } = new List<Chore>();
        public ICollection<MaintenanceTask> MaintenanceTasks { get; set; } = new List<MaintenanceTask>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}