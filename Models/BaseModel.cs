using System.ComponentModel.DataAnnotations;

namespace Base_API_Structure.Models
{
    public record BaseModel 
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string field1 { get; init; }

        [Required]
        public string field2 { get; init; }
        
        [Required]
        public string field3 { get; init; }
    
    }
}