﻿using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;

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
       // public Random rnd = new Random();
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOperationService _operation;
        
        public StudySetsService(GraspItEzContext dbContext, IMapper mapper, IOperationService operation)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _operation = operation;
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
            
            
            foreach (var question in studySetDto.Querist)
            {
                var query = studySet.Querist.FirstOrDefault(s => s.QueryId == question.QueryId);
                if (query != null)
                {

                    query.Question = question.Question;
                    query.Answer = question.Answer;
                    query.AnswerStatus = 0;
                    query.QuestionStatus = 0;
                    query.QueryStatusId = 0;

                }
                else
                {
                    question.AnswerStatus = 0;
                    question.QuestionStatus = 0;
                    question.QueryStatusId = 0;
                    studySet.Querist.Add(question);
                }
            }
            foreach (var question in studySet.Querist)
            {
                var deletedQuery = studySetDto.Querist.FirstOrDefault(s => s.QueryId == question.QueryId);
                if (deletedQuery == null) 
                {
                    studySet.Querist.Remove(question);
                }
            }




            studySet.Count = studySet.Querist.Count;
            studySet.Progress = _operation.Progress(studySet.Querist);
            studySet.LastUsed = DateTime.Now;
            studySet.Name = studySetDto.Name;
            studySet.Description = studySetDto.Description;

            return true;
            


        }


    }
}
