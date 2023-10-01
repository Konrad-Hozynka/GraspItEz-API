using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Services
{
    public interface IStudySetsService
    {
        public IEnumerable<StudySetHeadsDto> GetLastUsedStudySets();
        public IEnumerable<StudySetHeadsDto> GetAllStudySets();
        public StudySetDto GetById(int id);
        public int CreateStudySet(CreateStudySetDto dto);
        public bool DeleteStudySet(int id);
        public bool UpdateStudySet(UpdateStudySetDto Dto);
    }
    public class StudySetsService : IStudySetsService
    {
       // public Random rnd = new Random();
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        
        public StudySetsService(GraspItEzContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }

        public IEnumerable<StudySetHeadsDto> GetLastUsedStudySets()
        {
            var studySet = _dbContext.StudySets.ToList();
            studySet.Sort((x, y) => x.LastUsed.CompareTo(y.LastUsed));
            var lastUsedStudySets = _mapper.Map<List<StudySetHeadsDto>>(studySet.Take(6));
            return lastUsedStudySets;
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
            }
            _dbContext.StudySets .Add(studySet);
            _dbContext.SaveChanges();
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
            var studySetDto = _mapper.Map<StudySet>(dto);
            var studySet = _dbContext.StudySets
                .Include(s => s.Querist)
                .FirstOrDefault(s => s.StudySetId == dto.StudySetId);
            if (studySet is null) return false;
            studySet.Querist.Clear();
            
            foreach (var question in studySetDto.Querist)
            {
                studySet.Querist.Add(question);
            }




            studySet.Count = studySet.Querist.Count;
            //create function to set progress
            studySet.LastUsed = DateTime.Now;
            studySet.Created = studySetDto.Created;
            studySet.Name = studySetDto.Name;
            studySet.Description = studySetDto.Description;

            return true;
            


        }


    }
}
