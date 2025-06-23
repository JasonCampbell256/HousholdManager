using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Models
{
    public class Expense
    {
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; } = DateTime.Now;
        
        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? Category { get; set; }
        
        // Foreign key
        public int HouseholdId { get; set; }
        public Household Household { get; set; } = null!;
    }
}