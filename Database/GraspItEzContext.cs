using Microsoft.EntityFrameworkCore;
namespace GraspItEz.Database
{
    public class GraspItEzContext : DbContext
    {
        public GraspItEzContext(DbContextOptions<GraspItEzContext> options) : base(options) 
        {

        }
        public DbSet<StudySet> StudySets { get; set; }
        public DbSet<Query> Querist { get; set; }
        public DbSet<QueryStatus> QueryStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudySet>()
                .Property(s => s.Count)
                .IsRequired();

            modelBuilder.Entity<StudySet>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<StudySet>()
                .Property(s => s.Description)
                .HasMaxLength(500);
            modelBuilder.Entity<StudySet>()
                .Property(s => s.Created)
                .IsRequired();
            modelBuilder.Entity<StudySet>()
                .Property(s => s.Progress)
                .IsRequired();
            modelBuilder.Entity<StudySet>()
                .Property(s => s.LastUsed)
                .IsRequired();
           
            modelBuilder.Entity<Query>()
                .Property(s => s.Question)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Query>()
               .Property(s => s.Answer)
               .IsRequired()
               .HasMaxLength(200);
         
            modelBuilder.Entity<Query>()
                .Property(s => s.QuestionStatus)
                .IsRequired();
            modelBuilder.Entity<Query>()
                .Property (s => s.AnswerStatus)
                .IsRequired();
            modelBuilder.Entity<QueryStatus>()
                .Property(s => s.QueryStatusId)
                .IsRequired();
           
            modelBuilder.Entity<QueryStatus>()
                .Property(s => s.QueryStatusValue)
                .IsRequired();

        }
   
    }
}
