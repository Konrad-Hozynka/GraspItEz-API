using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Services
{
    public interface IStudySetsService
    {
        public IEnumerable<StudySetHeadsDto> GetStudySetsHeder();
        public IEnumerable<StudySetHeadsDto> GetAllStudySets();
        public StudySetDto GetById(int id);
        public int CreateStudySet(CreateStudySetDto dto);
        public bool DeleteStudySet(int id);
        public bool UpdateStudySet(UpdateStudySetDto Dto);
    }
    public class StudySetsService : IStudySetsService
    {
        public Random rnd = new Random();
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILearnLogicService _learnLogicService;
        public StudySetsService(GraspItEzContext dbContext, IMapper mapper, ILearnLogicService learnLogicService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _learnLogicService = learnLogicService;
        }

        public IEnumerable<StudySetHeadsDto> GetStudySetsHeder()
        {
            var studySet = _dbContext.StudySets.ToList();
            studySet.Sort((x, y) => x.LastUsed.CompareTo(y.LastUsed));
            var studySetsHead = _mapper.Map<List<StudySetHeadsDto>>(studySet.Take(6));
            return studySetsHead;
        }
        public IEnumerable<StudySetHeadsDto> GetAllStudySets()
        {
            var studySet = _dbContext.StudySets.ToList();
            studySet.Sort((x, y) => x.LastUsed.CompareTo(y.LastUsed));
            var studySetsHead = _mapper.Map<List<StudySetHeadsDto>>(studySet);
            return studySetsHead;
        }
        public StudySetDto GetById(int id)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
                
            var studySetsDto = _mapper.Map<StudySetDto>(studySet);
            return studySetsDto;
        }
        public int CreateStudySet(CreateStudySetDto dto)
        {
            var studySet = _mapper.Map<StudySet>(dto);
            studySet.Progress = 0;
            studySet.Count = studySet.Questions.Count;
            foreach (var question in studySet.Questions) 
            {
                question.QuestStatus = 0;
                question.DefinitionStatus = 0;
                question.IsActive = false;
                question.IsLearned = false;
            }
            _dbContext.StudySets .Add(studySet);
            _dbContext.SaveChanges();
            return studySet.Id;
        }
        public bool DeleteStudySet(int id)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
            if (studySet is null) return false;
            _dbContext.StudySets.Remove(studySet);
            _dbContext.SaveChanges ();
            return true;
        }
        public bool UpdateStudySet(UpdateStudySetDto Dto)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == Dto.Id);
            if (studySet is null) return false;
            // dodaje i edytuje słówka na wzór tych z dto
            foreach (var item in Dto.Questions)
            {
                var question = _dbContext.Questions
                    .FirstOrDefault(q => q.Id == item.Id);
                if (question is null)
                {
                    Question newQuestion = new Question();
                    newQuestion.IsActive = false;
                    newQuestion.IsLearned = false;
                    newQuestion.Quest = item.Quest;
                    newQuestion.QuestStatus = 0;
                    newQuestion.DefinitionStatus = 0;
                    newQuestion.Definition = item.Definition;
                    newQuestion.StudySetId = Dto.Id;
                    studySet.Questions.Add(newQuestion);
                    _dbContext.SaveChanges();
                }
                else
                {
                    question.Quest = item.Quest;
                    question.Definition =item.Definition;
                    _dbContext.SaveChanges();
                }
                
            }
            // usuwa wszystkie słówka które nie znajdują się w Dto
            foreach (var item in studySet.Questions.ToList()) 
            {
                var question = Dto.Questions
                      .FirstOrDefault(q => q.Id == item.Id);
                if (question is null)
                {
                    studySet.Questions.Remove(item);
                    _dbContext.SaveChanges();
                }

            }
            studySet.Name = Dto.Name;
            studySet.Count = studySet.Questions.Count;
            _learnLogicService.SetProgress(Dto.Id);
            studySet.Description = Dto.Description;
            studySet.LastUsed = Dto.LastUsed;
            _dbContext.SaveChanges();

            return true;
        }


    }
}
