using System.ComponentModel.DataAnnotations;

namespace GraspItEz.Models
{
    public class StudySetHeadLineDto
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
        public string Description { get; set;}

    }
}
