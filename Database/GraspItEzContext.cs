using Microsoft.EntityFrameworkCore;
namespace GraspItEz.Database
{
    public class GraspItEzContext : DbContext
    {
        public GraspItEzContext(DbContextOptions<GraspItEzContext> options) : base(options) 
        {

        }
        public DbSet<StudySet> StudySets { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionStatus> QuestionStatuses { get; set; }

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
           
            modelBuilder.Entity<Question>()
                .Property(s => s.Quest)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Question>()
               .Property(s => s.Definition)
               .IsRequired()
               .HasMaxLength(150);

        }
   
    }
}
