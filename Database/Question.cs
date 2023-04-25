namespace GraspItEz.Database
{
    public class Question
    {
        public int Id { get; set; }
        public string Quest { get; set; }
        public string Definition { get; set; }
        public bool IsActive { get; set; }
        public bool IsLearned { get; set; }
        public int QuestStatus { get; set; }
        public int DefinitionStatus { get; set; }
        public int StudySetId { get; set; }
        
    }
}
