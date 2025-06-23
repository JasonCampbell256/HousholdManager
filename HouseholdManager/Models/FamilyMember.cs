using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class FamilyMember
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string? Role { get; set; } // e.g., "Parent", "Child"
        
        public DateTime? DateOfBirth { get; set; }
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
        
        // Navigation properties
        public ICollection<Chore> AssignedChores { get; set; } = new List<Chore>();
    }
}