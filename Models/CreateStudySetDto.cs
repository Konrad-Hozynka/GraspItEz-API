using System.ComponentModel.DataAnnotations;
using GraspItEz.Database;

namespace GraspItEz.Models
{
    public class CreateStudySetDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public virtual List<QueryDto> Querist { get; set; }
    }
}
