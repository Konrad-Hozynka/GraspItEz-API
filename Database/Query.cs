using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraspItEz.Database
{
    public class Query
    {
        
        public int QueryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Question { get; set; }
        [Required]
        [MaxLength(200)]
        public string Answer { get; set; }
        [Required]
        [Range(-2,2)]
        public int QuestionStatus { get; set; }
        [Required]
        [Range(-2, 2)]
        public int AnswerStatus { get; set; }
        [Required]
        [Range(1, 3)]
        public int QueryStatusId { get; set; }
        [Required]
        public int StudySetId { get; set; }
        
    }
}
