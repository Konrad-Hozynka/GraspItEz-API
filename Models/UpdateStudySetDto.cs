using GraspItEz.Database;

namespace GraspItEz.Models
{
    public class UpdateStudySetDto
    {
        public int StudySetId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public virtual List<QuestionDto> Questions { get; set; }
        public virtual List<Query> ActiveQuestions { get; set; }
        public virtual List<Query> LernedQuestions { get; set; }
    }
}
