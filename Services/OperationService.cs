﻿using AutoMapper;
using GraspItEz.Database;

namespace GraspItEz.Services
{
    public interface IOperationService 
    {
        public int Progress(List<Query> querist);
    }
    public class OperationService : IOperationService
    {
        private readonly GraspItEzContext _dbContext;
        

        public OperationService(GraspItEzContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Progress(List <Query> querist)
        {
            int pointsPool = querist.Count * 2;
            int sum = 0;
            foreach (Query question in querist) 
            {
                if (question.QueryStatusId == 2)
                {
                    sum = sum + 2;
                }
                else if (question.QuestionStatus == 2 || question.AnswerStatus == 2)
                {
                    sum = sum + 1;
                }
            }
            return sum / pointsPool * 100;

            
        }
    }
}
