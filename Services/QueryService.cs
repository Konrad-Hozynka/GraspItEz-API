using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Services
{
    public interface IQueryService
    {
        public int NewQuery(int studySetId, QueryDto dto);
        public bool UpdateQuery(int studySetId, QueryWithIdDto dto);
        public bool DeleteQuery(int studySetId, int QueryId);


    }
    public class QueryService : IQueryService
    {
        private readonly GraspItEzContext _dbContext;
        private readonly IOperationService _operation;


        public QueryService(GraspItEzContext dbContext, IOperationService operation)
        {
            _dbContext = dbContext;
            _operation = operation;
        }
        public int NewQuery(int studySetId, QueryDto dto)
        {
            Query newQuery = new()
            {
                StudySetId = studySetId,
                Answer = dto.Answer,
                Question = dto.Question,
                QueryStatusId = 1,
                AnswerStatus = 0,
                QuestionStatus = 0
            };
            var studySet = _dbContext.StudySets
                .Include(s => s.Querist)
                .FirstOrDefault(s => s.StudySetId == studySetId);
            studySet.Querist.Add(newQuery);
            studySet.Progress = _operation.Progress(studySet.Querist);
            studySet.Count = studySet.Querist.Count;
            studySet.LastUsed = DateTime.Now;
            _dbContext.SaveChanges();
            return newQuery.QueryId;
        }
        public bool UpdateQuery(int studySetId, QueryWithIdDto dto)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Querist)
                .FirstOrDefault(s => s.StudySetId == studySetId);
            var query =studySet.Querist
                .FirstOrDefault(q => q.QueryId == dto.QueryId);
            if (query is null) return false;
            query.Answer = dto.Answer;
            query.Question = dto.Question;
            query.QueryStatusId = 1;
            query.AnswerStatus = 0;
            query.QuestionStatus = 0;
            studySet.Progress = _operation.Progress(studySet.Querist);
            studySet.LastUsed = DateTime.Now;
            _dbContext.SaveChanges();
            
            return true;
        }
        public bool DeleteQuery(int studySetId, int QueryId) 
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Querist)
                .FirstOrDefault(s => s.StudySetId == studySetId);
            var query = studySet.Querist
                .FirstOrDefault(q => q.QueryId == QueryId);
            if (query is null) return false;
            studySet.Querist.Remove(query);
            studySet.Progress = _operation.Progress(studySet.Querist);
            studySet.Count = studySet.Querist.Count;
            studySet.LastUsed = DateTime.Now;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
