using System.ComponentModel.DataAnnotations;
using GraspItEz.Database;

namespace GraspItEz.Models
{
    public class StudySetDto
    {
        public int StudySetId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        [Range(0,100)]
        public int Progres { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime LastUsed { get; set; }
        public virtual List<QueryWithIdDto> Querist { get; set; }
       
    }
}
