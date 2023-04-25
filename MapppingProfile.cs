using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;

namespace GraspItEz
{
    public class MapppingProfile : Profile
    {   public MapppingProfile() 
        {
            CreateMap<StudySet, StudySetHeadsDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<StudySet, StudySetDto>();
            
        }

    }
}
