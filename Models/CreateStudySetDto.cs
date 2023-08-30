using GraspItEz.Database;

namespace GraspItEz.Models
{
    public class CreateStudySetDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual List<QuestionDto> Questions { get; set; }
    }
}
