using System.ComponentModel.DataAnnotations;

namespace Base_API_Structure.DTOs
{
    public record BaseReadDTO 
    {
        public int Id { get; init; }
        public string field1 { get; init; }
        public string field2 { get; init; }
        public string field3 { get; init; }
    }
}