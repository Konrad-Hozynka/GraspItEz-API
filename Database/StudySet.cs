using System.Net;

namespace GraspItEz.Database
{
    public class StudySet
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int Progress { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUsed{ get; set; }
        public virtual List<Question> Questions { get; set; }
       
    }

}
