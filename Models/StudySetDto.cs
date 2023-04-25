using GraspItEz.Database;

namespace GraspItEz.Models
{
    public class StudySetDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int Progres { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public virtual List<QuestionDto> Questions { get; set; }
       
    }
}
