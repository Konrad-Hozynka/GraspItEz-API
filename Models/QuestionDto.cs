namespace GraspItEz.Models
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Quest { get; set; }
        public string Definition { get; set; }
        public int QestStatus { get; set; }
        public int DefinitionStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsLearned { get; set; }
    }
}
