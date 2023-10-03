using System.ComponentModel.DataAnnotations;

namespace GraspItEz.Models
{
    public class QueryDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Quest { get; set; }
        [Required]
        [MaxLength(200)]
        public string Definition { get; set; }
        
    }
}
