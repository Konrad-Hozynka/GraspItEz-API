using System.ComponentModel.DataAnnotations;

namespace GraspItEz.Models
{
    public class QueryWithIdDto
    {
        [Required]
        public int QueryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Question { get; set; }
        [Required]
        [MaxLength(200)]
        public string Answer { get; set; }

        
    }

}

