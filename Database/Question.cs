namespace GraspItEz.Database
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Quest { get; set; }
        public string Definition { get; set; }
        public int QuestStatus { get; set; }
        public int DefinitionStatus { get; set; }
        public int QuestionStatusId { get; set; }
        public int StudySetId { get; set; }
        
    }
}
