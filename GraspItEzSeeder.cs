using GraspItEz.Database;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz
{
    public class GraspItEzSeeder
    {
        private readonly GraspItEzContext _dbContext;
        public GraspItEzSeeder(GraspItEzContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect()) 
            {
                var migrations = _dbContext.Database.GetPendingMigrations();
                if (migrations != null && migrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.StudySets.Any())
                {
                    var studySets = GetStudySet();
                    _dbContext.StudySets.AddRange(studySets);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<StudySet> GetStudySet()
        {
            var studySets = new List<StudySet>()
            {
                new StudySet()
                {
                    Name = "Miesiące po hiszpańsku",
                    Progress = 0,
                    Description ="zestaw do nauki miesięcy po hiszpańsku domyślnie stworzy jako przykład",
                    Created = DateTime.Now,
                    LastUsed = DateTime.Now,
                    Questions = new List<Question>()
                    {
                        new Question()
                        {
                            Quest = "enero",
                            Definition = "styczeń"
                            
                        },
                         new Question()
                        {
                            Quest = "febrero",
                            Definition = "luty"
                            
                        },
                         new Question()
                        {
                            Quest = "marzo",
                            Definition = "marzec"
                            
                        },
                         new Question()
                        {Quest = "abril", Definition = "kwiecień", QuestStatus=0, DefinitionStatus=0, IsActive=false,IsLearned=false},
                         new Question()
                        {Quest = "mayo", Definition = "maj", QuestStatus=0, DefinitionStatus=0, IsActive=false, IsLearned=false},
                         new Question()
                        {Quest = "junio", Definition = "czerwiec", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "julio", Definition = "lipiec", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "agosto", Definition = "sierpień", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "septiembre", Definition = "wrzesień", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "octubre", Definition = "pażdziernik", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "noviembre", Definition = "listopad", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "diciembre", Definition = "grudzień", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false}

                    },
                    Count = 12

                },
                new StudySet()
                {
                     Name = "Kolory po angieslku",
                    Progress = 0,
                    Description ="zestaw do nauki kolorów po hiszpańsku domyślnie stworzy jako przykład",
                    Count = 13,
                    Created = DateTime.Now,
                    LastUsed = DateTime.Now,
                    Questions = new List<Question>()
                    {
                         new Question()
                        {Quest = "white", Definition = "biały", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "black", Definition = "czarny", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "gray", Definition = "szary", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "yellow", Definition = "żółty", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "brown", Definition = "brązowy", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                          new Question()
                        {Quest = "orange", Definition = "pomarańczowy", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "red", Definition = "czerwony", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "pink", Definition = "różowy", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false },
                         new Question()
                        {Quest = "purple", Definition = "fioletowy", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "blue", Definition = "niebieski", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                          new Question()
                        {Quest = "green", Definition = "zielony", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question() 
                        {Quest = "gold", Definition = "złoty", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false},
                         new Question()
                        {Quest = "silver", Definition = "srebrny", QuestStatus = 0, DefinitionStatus = 0, IsActive = false, IsLearned = false}
                    }
                }

            };
            return studySets;
        }
    }
}
