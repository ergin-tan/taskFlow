using AutoMapper;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Models;

namespace TaskFlow.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<UpdateUserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Office, OfficeResponseDto>().ReverseMap();
            CreateMap<OfficeRequestDto, Office>();

            CreateMap<WorkTask, WorkTaskResponseDto>().ReverseMap();
            CreateMap<WorkTaskRequestDto, WorkTask>();

            CreateMap<TaskAssignment, TaskAssignmentResponseDto>().ReverseMap();
            CreateMap<TaskAssignmentRequestDto, TaskAssignment>();

            CreateMap<TaskHistory, TaskHistoryResponseDto>().ReverseMap();
            CreateMap<TaskHistoryRequestDto, TaskHistory>();
        }
    }
}
