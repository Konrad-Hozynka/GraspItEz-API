using AutoMapper;
using GraspItEz.Database;
using GraspItEz.Models;

namespace GraspItEz
{
    public class MapppingProfile : Profile
    {   public MapppingProfile() 
        {
            CreateMap<StudySet, StudySetHeadLineDto>();
            CreateMap<Query, QueryDto>();
            CreateMap<StudySet, StudySetDto>();
            CreateMap<CreateStudySetDto, StudySet>();
            CreateMap<QueryDto, Query>();
            CreateMap<QueryWithIdDto, Query>();
            CreateMap<Query, QueryWithIdDto>();
        }

    }
}
