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
    }
    public class StudySetsService : IStudySetsService
    {
        public Random rnd = new Random();
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        public StudySetsService(GraspItEzContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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


    }
}
