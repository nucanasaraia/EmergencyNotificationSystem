using AutoMapper;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;

namespace EmergencyNotifRespons.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UpdateUser>().ReverseMap();
            CreateMap<User, UserDto>();

            // Volunteer
            CreateMap<AddVolunteer, Volunteer>();
            CreateMap<Volunteer, VolunteerDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.User.Location));
            CreateMap<Volunteer, UpdateVolunteerDto>().ReverseMap();
            CreateMap<Volunteer, VolunteersDetailDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.User.Location));

            // EmergencyEvent
            CreateMap<AddEmergencyEvent, EmergencyEvent>()
                .ForMember(dest => dest.EVENT_TYPE, opt => opt.MapFrom(src => src.EventType))
                .ForMember(dest => dest.ACTIVITY_STATUS, opt => opt.MapFrom(src => ACTIVITY_STATUS.ACTIVE));
            CreateMap<EmergencyEvent, EmergencyEventDto>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EVENT_TYPE))
                .ForMember(dest => dest.ActivityStatus, opt => opt.MapFrom(src => src.ACTIVITY_STATUS))
                .ForMember(dest => dest.CreatedByName,
                    opt => opt.MapFrom(src => src.CreatedBy.FirstName + " " + src.CreatedBy.LastName));

            // EmergencyNotification
            CreateMap<AddNotifications, EmergencyNotification>();
            CreateMap<EmergencyNotification, NotificationDto>();

            // UserNotification 
            CreateMap<AddUSersNotifications, UserNotification>();
            CreateMap<UserNotification, NotificationDto>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Notification.Message))
                .ForMember(dest => dest.EmergencyEventId, opt => opt.MapFrom(src => src.Notification.EmergencyEventId))
                .ForMember(dest => dest.NotificationType, opt => opt.MapFrom(src => src.Notification.NotificationType))
                .ForMember(dest => dest.SentTime, opt => opt.MapFrom(src => src.Notification.SentTime))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => src.IsRead));

            // Resource
            CreateMap<AddResource, Resource>()
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<Resource, ResourceDto>();

            CreateMap<AddResourceAssignment, ResourceAssignment>();
            CreateMap<AddVolunteerAssignment, VolunteerAssignment>();
        }
    }
}