namespace GraspItEz.Models
{
    public class CreateStudySetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public virtual List<QuestionDto> Questions { get; set; }

    }
}
