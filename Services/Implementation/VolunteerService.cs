using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EmergencyNotifRespons.Services.Implementation
{
    public class VolunteerService : IVolunteerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VolunteerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> AssignToEvent(int volunteerId, int eventId, int assignedById)
        {
            try
            {
                var volunteer = await _context.Volunteers
                .Where(v => v.Id == volunteerId)
                .Include(v => v.VolunteerAssignments)
                .FirstOrDefaultAsync();

                if (volunteer == null)
                {
                    return ApiResponseFactory.NotFound<string>("volunteer not found");
                }

                if (volunteer.AvailabilityStatus == AVAILABILITY_STATUS.ON_MISSION ||
                    volunteer.AvailabilityStatus == AVAILABILITY_STATUS.UNAVAILABLE)
                {
                    return ApiResponseFactory.BadRequest<string>("Volunteer is not available for assignment");
                }

                volunteer.VolunteerAssignments.Add(new VolunteerAssignment
                {
                    EmergencyEventId = eventId,
                    AssignedById = assignedById,
                    AssignedTime = DateTime.UtcNow,
                });
                volunteer.AvailabilityStatus = AVAILABILITY_STATUS.ON_MISSION; 

                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("volunteer assigned to event successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while assigning volunteer to event: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> CompleteAssignment(int volunteerId, int assignmentId)
        {
            try
            {
                var volunteerAssignment = await _context.VolunteerAssignments
                .FirstOrDefaultAsync(v => v.VolunteerId == volunteerId && v.Id == assignmentId);

                if (volunteerAssignment == null)
                {
                    return ApiResponseFactory.NotFound<string>("volunteer or assignment not found");
                }

                volunteerAssignment.CompletedTime = DateTime.UtcNow;
                volunteerAssignment.Status = VOLUNTEER_ASSIGNMENT_STATUS.COMPLETED;
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("volunteer assignment completed successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while completing volunteer assignment: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<VolunteerDto>> GetVolunteerProfile(int userId)
        {
            try
            {
                var volunteer = await _context.Volunteers
                        .Include(v => v.User)  
                        .FirstOrDefaultAsync(v => v.UserId == userId);
                if (volunteer == null)
                {
                    return ApiResponseFactory.NotFound<VolunteerDto>("volunteer not found");
                }
                var volunteerDto = _mapper.Map<VolunteerDto>(volunteer);
                return ApiResponseFactory.Success(volunteerDto);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<VolunteerDto>("An error occurred while retrieving volunteer profile: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<VolunteerDto>>> GetVolunteersByAvailability(AVAILABILITY_STATUS status)
        {
            try
            {
                var volunteers = await _context.Volunteers
                    .Include(v => v.User)   
                    .Where(v => v.AvailabilityStatus == status)
                    .ToListAsync();

                var volunteerDtos = _mapper.Map<List<VolunteerDto>>(volunteers);
                return ApiResponseFactory.Success(volunteerDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<VolunteerDto>>("An error occurred while retrieving volunteers by availability: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> RegisterAsVolunteer(int userId, AddVolunteer request)
        {
            try
            {
                var existing = await _context.Volunteers.AnyAsync(v => v.UserId == userId);
                if (existing)
                    return ApiResponseFactory.Conflict<string>("User is already registered as a volunteer");

                var volunteer = _mapper.Map<Volunteer>(request);
                volunteer.UserId = userId;

                await _context.Volunteers.AddAsync(volunteer);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Volunteer registered successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while registering volunteer: "
                    + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> UpdateAvailability(int volunteerId, AVAILABILITY_STATUS status)
        {
            try
            {
                var volunteer = await _context.Volunteers.FirstOrDefaultAsync(v => v.Id == volunteerId);
                if (volunteer == null)
                {
                    return ApiResponseFactory.NotFound<string>("volunteer not found");
                }

                volunteer.AvailabilityStatus = status;
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("volunteer availability updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while updating volunteer availability: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
