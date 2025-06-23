using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        public ChoreFrequency Frequency { get; set; }
        
        public DateTime? LastCompletedDate { get; set; }
        
        public DateTime? NextDueDate { get; set; }
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
    }
}