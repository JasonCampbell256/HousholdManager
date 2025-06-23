using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class Note
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedDate { get; set; }
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
        
        public int? CreatedById { get; set; }
        public FamilyMember? CreatedBy { get; set; }
    }
}