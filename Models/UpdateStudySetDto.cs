namespace GraspItEz.Models
{
    public class UpdateStudySetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime LastUsed { get; set; }
        public virtual List<QuestionDto> Questions { get; set; }
    }
}
