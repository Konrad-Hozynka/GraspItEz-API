using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;

namespace GraspItEz
{
    public class MapppingProfile : Profile
    {   public MapppingProfile() 
        {
            CreateMap<StudySet, StudySetHeadsDto>();
            CreateMap<Query, QuestionDto>();
            CreateMap<StudySet, StudySetDto>();
            CreateMap<CreateStudySetDto, StudySet>();
            CreateMap<QuestionDto, Query>();
        }

    }
}
