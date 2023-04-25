using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;
using Microsoft.EntityFrameworkCore;

namespace GraspItEz.Services
{
    public interface IGraspItEzService
    {
        public IEnumerable<StudySetHeadsDto> GetStudySetsHeder();
        public IEnumerable<StudySetHeadsDto> GetAllStudySets();
        public StudySetDto GetById(int id);
        public IEnumerable<QuestionDto> StartLearn(int id);
        public void EndOfRound(int id, List<QuestionAnswer> answer);
        public void Reset(int id);

    }
    public class GraspItEzService : IGraspItEzService
    {
        public Random rnd = new Random();
        private readonly GraspItEzContext _dbContext;
        private readonly IMapper _mapper;
        public GraspItEzService(GraspItEzContext dbContext, IMapper mapper)
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
        public List<Question> ListOfQById(int id)
        {
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
            var listQuestions = studySet.Questions.ToList();
            return listQuestions;
        }
        public void AddToActive(int id, int times)
        {
            int index;
            int i = 0;
            var listOfNoActiveQuestions = ListOfQById (id);
            foreach (var questionn in listOfNoActiveQuestions.ToList())
            {
                if (questionn.IsActive == true )
                {
                    listOfNoActiveQuestions.Remove(questionn);
                }
            }
            if (listOfNoActiveQuestions.Count < times) times = listOfNoActiveQuestions.Count;
            while (i<times)
            {
                index=rnd.Next(listOfNoActiveQuestions.Count);
                if ((listOfNoActiveQuestions[index].IsActive == false) && (listOfNoActiveQuestions[index].IsLearned==false))
                {
                    listOfNoActiveQuestions[index].IsActive = true;
                    i++;
                }
            }
            _dbContext.SaveChanges();

        }
        
        public List<Question> ActiveQuestionsList(int id)
        {
            
            var activeQuestion = ListOfQById (id);
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
                if ((questionn.IsActive == false) || (questionn.IsLearned==true))
                {
                    activeQuestion.Remove(questionn);
                }
            }
            return activeQuestion;
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
        public IEnumerable<QuestionDto> StartLearn(int id)
        {
            return RoundQuestionSet(id);
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
                            SetProgress(id);
                        }
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        question.DefinitionStatus = question.DefinitionStatus++;
                        if (question.QuestStatus == 2 && question.DefinitionStatus == 2)
                        {
                            question.IsLearned = true;
                            SetProgress(id);
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
        }
        public void SetProgress(int id)
        {
            var learned = ListOfQById(id);
            foreach (var question in learned) 
            {
                if (question.IsLearned==false)
                {
                    learned.Remove(question);
                }

            }
            var studySet = _dbContext.StudySets
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == id);
            studySet.Progress =learned.Count / studySet.Count * 100;
            _dbContext.SaveChanges();
        }

    }
}
