using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Database
{
    public class StudySet
    {
        
        public int StudySetId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        [Range(0,100)]
        public int Progress { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime LastUsed{ get; set; }
        public virtual List<Query> Querist { get; set; }
        

    }

}
