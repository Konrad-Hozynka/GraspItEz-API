using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace GraspItEz.Services
{
    public interface IStudySetsService
    {
        public IEnumerable<StudySetHeadLineDto> GetLastUsedStudySets();
        public IEnumerable<StudySetHeadLineDto> GetAllStudySets();
        public StudySetDto GetById(int id);
        public int CreateStudySet(CreateStudySetDto dto);
        public bool DeleteStudySet(int id);
        public bool UpdateStudySet(UpdateStudySetDto Dto);
    }
    public class StudySetsService : IStudySetsService
    {
        private readonly ILogger<StudySetsService> _logger;
        // public Random rnd = new Random();
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOperationService _operation;
        
        public StudySetsService(GraspItEzContext dbContext, IMapper mapper, IOperationService operation, ILogger<StudySetsService> loger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _operation = operation;
            _logger = loger;

        }

        public IEnumerable<StudySetHeadLineDto> GetLastUsedStudySets()
        {
            var studySet = _dbContext.StudySets.ToList();
            studySet.Sort((x, y) => x.LastUsed.CompareTo(y.LastUsed));
            var lastUsedStudySets = _mapper.Map<List<StudySetHeadLineDto>>(studySet.Take(6));
            return lastUsedStudySets;
        }
        public IEnumerable<StudySetHeadLineDto> GetAllStudySets()
        {
            var studySet = _dbContext.StudySets.ToList();
            studySet.Sort((x, y) => x.LastUsed.CompareTo(y.LastUsed));
            var studySetsHead = _mapper.Map<List<StudySetHeadLineDto>>(studySet);
            return studySetsHead;
        }
        public StudySetDto GetById(int id)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Querist)
                .FirstOrDefault(s => s.StudySetId == id);
                
            var studySetsDto = _mapper.Map<StudySetDto>(studySet);
            return studySetsDto;
        }
        public int CreateStudySet(CreateStudySetDto dto)
        {
            
            var studySet = _mapper.Map<StudySet>(dto);
            studySet.Progress = 0;
            studySet.Count = studySet.Querist.Count;
            studySet.Created = DateTime.Now;
            studySet.LastUsed = DateTime.Now;
            foreach (var question in studySet.Querist) 
            {
                question.QuestionStatus = 0;
                question.AnswerStatus = 0;
                question.QueryStatusId = 1;
            }
            _dbContext.StudySets .Add(studySet);
            _dbContext.SaveChanges();
            _logger.LogInformation("stworzono nowy zasob");
            return studySet.StudySetId;
        }
        public bool DeleteStudySet(int id)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Querist)
                .FirstOrDefault(s => s.StudySetId == id);
            if (studySet is null) return false;
            _dbContext.StudySets.Remove(studySet);
            _dbContext.SaveChanges ();
            return true;
        }
        public bool UpdateStudySet(UpdateStudySetDto dto)
        {
            var studySet = _dbContext.StudySets
                 .FirstOrDefault(s => s.StudySetId == dto.StudySetId);
            if (studySet is null) return false;
            studySet.Description = dto.Description;
            studySet.Name = dto.Name;
            studySet.LastUsed = DateTime.Now;
            _dbContext.SaveChanges();
            return true;
        }


    }
}
