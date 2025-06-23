using System.ComponentModel.DataAnnotations;

namespace HouseholdManager.Models
{
    public class PetCareTask
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        
        public DateTime Date { get; set; } = DateTime.Now;
        
        public bool IsCompleted { get; set; }
        
        [MaxLength(200)]
        public string? Notes { get; set; }
        
        // Foreign key
        public int PetId { get; set; }
        public Pet Pet { get; set; } = null!;
    }
}