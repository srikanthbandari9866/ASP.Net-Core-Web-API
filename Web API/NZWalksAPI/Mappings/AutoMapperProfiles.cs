using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<Region,AddRegionDto>().ReverseMap();
            CreateMap<Region,UpdateRegionDto>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Walk,AddWalkDto>().ReverseMap();
            CreateMap<Walk,UpdateWalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<User,UserRegisterDto>().ReverseMap();
            CreateMap<User,UserLoginDto>().ReverseMap();
        }
    }
}
