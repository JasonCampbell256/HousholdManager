using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class Pet
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty; // e.g., "Dog", "Cat", "Bird"
        
        public DateTime? DateOfBirth { get; set; }
        
        [MaxLength(500)]
        public string? Notes { get; set; }
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
        
        // Navigation properties
        public ICollection<PetCareTask> CareTasks { get; set; } = new List<PetCareTask>();
    }
}