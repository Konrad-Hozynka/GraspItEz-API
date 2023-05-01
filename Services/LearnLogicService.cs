using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Services
{
    public interface ILearnLogicService
    {
        public List<Question> ListOfQById(int id);
        public IEnumerable<QuestionDto> RoundQuestionSet(int id);
        public List<Question> ActiveQuestionsList(int id);
        public void AddToActive(int id, int times);
        public void SetProgress(int id);

    }
    public class LearnLogicService : ILearnLogicService
    {
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        public Random rnd = new Random();
        public LearnLogicService(GraspItEzContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<Question> ListOfQById(int id)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
            var listQuestions = studySet.Questions.ToList();
            return listQuestions;
        }
        public IEnumerable<QuestionDto> RoundQuestionSet(int id)
        {
            int index;
            var activeQuestions = ActiveQuestionsList(id);
            while (activeQuestions.Count > 8)
            {
                index = rnd.Next(activeQuestions.Count);
                activeQuestions.Remove(activeQuestions[index]);
            }
            var questionSet = _mapper.Map<List<QuestionDto>>(activeQuestions);
            return questionSet;
        }
        public List<Question> ActiveQuestionsList(int id)
        {

            var activeQuestion = ListOfQById(id);
            foreach (var questionn in activeQuestion.ToList())
            {
                if ((questionn.IsActive == false) || (questionn.IsLearned == true))
                {
                    activeQuestion.Remove(questionn);
                }
            }
            if (activeQuestion.Count < 15)
            {
                int times = 15 - activeQuestion.Count;
                AddToActive(id, times);
            }
            activeQuestion = ListOfQById(id);
            foreach (var questionn in activeQuestion)
            {
                if ((questionn.IsActive == false) || (questionn.IsLearned == true))
                {
                    activeQuestion.Remove(questionn);
                }
            }
            return activeQuestion;
        }
        public void AddToActive(int id, int times)
        {
            int index;
            int i = 0;
            var listOfNoActiveQuestions = ListOfQById(id);
            foreach (var questionn in listOfNoActiveQuestions.ToList())
            {
                if (questionn.IsActive == true)
                {
                    listOfNoActiveQuestions.Remove(questionn);
                }
            }
            if (listOfNoActiveQuestions.Count < times) times = listOfNoActiveQuestions.Count;
            while (i < times)
            {
                index = rnd.Next(listOfNoActiveQuestions.Count);
                if ((listOfNoActiveQuestions[index].IsActive == false) && (listOfNoActiveQuestions[index].IsLearned == false))
                {
                    listOfNoActiveQuestions[index].IsActive = true;
                    i++;
                }
            }
            _dbContext.SaveChanges();

        }
        public void SetProgress(int id)
        {
            var learned = ListOfQById(id);
            foreach (var question in learned.ToList())
            {
                if (question.IsLearned == false)
                {
                    learned.Remove(question);
                }

            }
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
            studySet.Progress = learned.Count / studySet.Count * 100;
            _dbContext.SaveChanges();
        }

    }

}
