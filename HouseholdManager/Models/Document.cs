using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class Document
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? FileType { get; set; } // e.g., "PDF", "Image", "Word"
        
        [MaxLength(100)]
        public string? Category { get; set; } // e.g., "Warranty", "Manual", "Receipt"
        
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
    }
}