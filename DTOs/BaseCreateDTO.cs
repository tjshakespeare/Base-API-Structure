using System.ComponentModel.DataAnnotations;

namespace Base_API_Structure.DTOs
{
    public record BaseCreateDTO 
    {
        [Required]
        public string field1 { get; init; }

        [Required]
        public string field2 { get; init; }
        
        [Required]
        public string field3 { get; init; }
    }
}