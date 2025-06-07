using AutoMapper;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;

namespace EmergencyNotifRespons.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, AddUser>().ReverseMap();

            CreateMap<Volunteer, AddVolunteer>().ReverseMap();
            CreateMap<Volunteer, UpdateVolunteerDto>().ReverseMap();
            CreateMap<Volunteer, VolunteersDetailDto>()
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
             .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.User.Location));

            CreateMap<EmergencyEvent, AddEmergencyEvent>().ReverseMap();
            CreateMap<EmergencyNotification, AddNotifications>().ReverseMap();
            CreateMap<UserNotification, AddUSersNotifications>().ReverseMap();
            CreateMap<Resource, AddResource>().ReverseMap();
            CreateMap<ResourceAssignment, AddResourceAssignment>().ReverseMap();
            CreateMap<VolunteerAssignment, AddVolunteerAssignment>().ReverseMap();

        }
    }
}
