using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Services
{
    public interface ILearnService
    {
        public IEnumerable<QuestionDto> StartLearn(int id);
        public void EndOfRound(int id, List<QuestionAnswer> answers);
        public void Reset(int id);
    }
    public class LearnService : ILearnService
    {
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILearnLogicService _learnLogicService;
        private readonly ILogger<LearnService> _logger;
        public LearnService(GraspItEzContext dbContext, IMapper mapper, ILearnLogicService learnLogicService, ILogger<LearnService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _learnLogicService = learnLogicService;
            _logger = logger;
        }
        
        public IEnumerable<QuestionDto> StartLearn(int id)
        {
            return _learnLogicService.RoundQuestionSet(id);
        }
        public void EndOfRound(int id, List<QuestionAnswer> answers)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
            foreach (var answer in answers)
            {
                var question = studySet.Questions.FirstOrDefault(q => q.Id == answer.Id);
                if (answer.Correct == true)
                {
                    if (question.QuestStatus != 2)
                    {
                        question.QuestStatus = question.QuestStatus++;
                        if (question.QuestStatus == 2 && question.DefinitionStatus == 2)
                        {
                            question.IsLearned = true;
                            _learnLogicService.SetProgress(id);
                        }
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        question.DefinitionStatus = question.DefinitionStatus++;
                        if (question.QuestStatus == 2 && question.DefinitionStatus == 2)
                        {
                            question.IsLearned = true;
                           _learnLogicService.SetProgress(id);
                        }
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    if (question.QuestStatus != 2)
                    {
                        question.QuestStatus = question.QuestStatus--;
                        if (question.QuestStatus == -3 || question.DefinitionStatus == -3)
                        {
                            question.IsActive = false;
                            question.IsLearned = false;
                            question.QuestStatus = 0;
                            question.DefinitionStatus = 0;
                        }
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        question.DefinitionStatus = question.DefinitionStatus--;
                        if (question.QuestStatus == -3 || question.DefinitionStatus == -3)
                        {
                            question.IsActive = false;
                            question.IsLearned = false;
                            question.QuestStatus = 0;
                            question.DefinitionStatus = 0;
                        }
                        _dbContext.SaveChanges();
                    }
                }

            }

        }
        public void Reset(int id)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
            studySet.Progress = 0;
            foreach (var question in studySet.Questions)
            {
                question.IsActive = false;
                question.IsLearned = false;
                question.QuestStatus = 0;
                question.DefinitionStatus = 0;
            }
            _dbContext.SaveChanges();
            _logger.LogInformation($"Study set with id: {id} has been reset");
        }
        
    }

}
