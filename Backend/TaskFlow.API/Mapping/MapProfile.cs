using AutoMapper;
using TaskFlow.API.DTOs;
using TaskFlow.Core.Models;

namespace TaskFlow.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office.OfficeName))
                .ReverseMap();
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<UpdateUserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Office, OfficeResponseDto>().ReverseMap();
            CreateMap<OfficeRequestDto, Office>();

            CreateMap<WorkTask, WorkTaskResponseDto>()
                .ForMember(dest => dest.AssignedByUserName, opt => opt.MapFrom(src => src.AssignedByUser != null ? src.AssignedByUser.FirstName + " " + src.AssignedByUser.LastName : null))
                .ReverseMap();
            CreateMap<WorkTaskRequestDto, WorkTask>();

            CreateMap<TaskAssignment, TaskAssignmentResponseDto>()
                .ForMember(dest => dest.WorkTaskTitle, opt => opt.MapFrom(src => src.WorkTask != null ? src.WorkTask.TaskTitle : null))
                .ForMember(dest => dest.AssignedToUserName, opt => opt.MapFrom(src => src.AssignedToUser != null ? src.AssignedToUser.FirstName + " " + src.AssignedToUser.LastName : null))
                .ReverseMap();
            CreateMap<TaskAssignmentRequestDto, TaskAssignment>();

            CreateMap<AuditLog, AuditLogResponseDto>().ReverseMap();
        }
    }
}