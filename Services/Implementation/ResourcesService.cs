using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Net;

namespace EmergencyNotifRespons.Services.Implementation
{
    public class ResourcesService : IResourcesService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ResourcesService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> AssignToEvent(int resourceId, int eventId, int assignedById)
        {
            try
            {
                var eventExists = await _context.EmergencyEvents.AnyAsync(e => e.Id == eventId);
                if (!eventExists)
                    return ApiResponseFactory.NotFound<string>("Event not found");

                var resource = await _context.Resources.FindAsync(resourceId);
                if (resource == null)
                    return ApiResponseFactory.NotFound<string>("Resource not found");

                if (resource.Status == RESOURCE_STATUS.IN_USE)
                    return ApiResponseFactory.BadRequest<string>("Resource is already in use");

                var assignment = new ResourceAssignment
                {
                    ResourceId = resourceId,
                    EmergencyEventId = eventId,
                    AssignedById = assignedById,
                    AssignedTime = DateTime.UtcNow,
                    Status = RESOURCE_ASSIGNMENT_STATUS.ASSIGNED
                };

                resource.Status = RESOURCE_STATUS.IN_USE;

                await _context.ResourceAssignments.AddAsync(assignment);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Resource assigned to event successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while assigning resource to event: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        public async Task<ApiResponse<ResourceDto>> CreateResource(AddResource request)
        {
            try
            {
                var resource = _mapper.Map<Resource>(request);
                await _context.Resources.AddAsync(resource);
                await _context.SaveChangesAsync();

                var resourceDto = _mapper.Map<ResourceDto>(resource);
                return ApiResponseFactory.Success(resourceDto, "Resource created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<ResourceDto>("An error occurred while creating resource: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> DeleteResource(int id)
        {
            try
            {
                var resource = await _context.Resources
                    .Include(r => r.ResourceAssignments)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (resource == null)
                    return ApiResponseFactory.NotFound<string>("Resource not found");

                if (resource.ResourceAssignments != null &&
                    resource.ResourceAssignments.Any(a => a.Status != RESOURCE_ASSIGNMENT_STATUS.RETURNED))
                    return ApiResponseFactory.BadRequest<string>(
                        "Cannot delete resource with active assignments. Return the resource first.");

                if (resource.ResourceAssignments != null)
                    _context.ResourceAssignments.RemoveRange(resource.ResourceAssignments);

                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success<string>("Resource deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while deleting resource: "
                    + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<ResourceDto>>> GetAvailableResources(RESOURCE_CATEGORY? category = null)
        {
            try
            {
                var resources = await _context.Resources
                        .Where(r => category == null || r.Category == category)
                        .ToListAsync();

                if (resources == null || resources.Count == 0)
                {
                    return ApiResponseFactory.NotFound<List<ResourceDto>>("No resources found");
                }

                var response = _mapper.Map<List<ResourceDto>>(resources);
                return ApiResponseFactory.Success(response, "Resources retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<ResourceDto>>("An error occurred while retrieving resources: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<ResourceDto>> GetResourceById(int id)
        {
            try
            {
                var resource = await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
                if (resource == null)
                {
                    return ApiResponseFactory.NotFound<ResourceDto>("Resource not found");
                }

                var response = _mapper.Map<ResourceDto>(resource);
                return ApiResponseFactory.Success(response, "Resource retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<ResourceDto>("An error occurred while retrieving resource: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> ReturnResource(int assignmentId)
        {
            try
            {
                var assignment = await _context.ResourceAssignments.FindAsync(assignmentId);
                if (assignment == null)
                    return ApiResponseFactory.NotFound<string>("Assignment not found");

                var resource = await _context.Resources.FindAsync(assignment.ResourceId);
                if (resource != null)
                    resource.Status = RESOURCE_STATUS.AVAILABLE;

                assignment.ReturnedTime = DateTime.UtcNow;
                assignment.Status = RESOURCE_ASSIGNMENT_STATUS.RETURNED;

                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Resource returned successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<ResourceAssignmentDto>>> GetResourceAssignments(int resourceId)
        {
            try
            {
                var assignments = await _context.ResourceAssignments
                    .Where(a => a.ResourceId == resourceId)
                    .ToListAsync();

                return ApiResponseFactory.Success(assignments.Select(a => new ResourceAssignmentDto
                {
                    Id = a.Id,
                    ResourceId = a.ResourceId,
                    EmergencyEventId = a.EmergencyEventId,
                    AssignedTime = a.AssignedTime,
                    ReturnedTime = a.ReturnedTime,
                    Status = a.Status == RESOURCE_ASSIGNMENT_STATUS.ASSIGNED ? RESOURCE_STATUS.IN_USE : RESOURCE_STATUS.AVAILABLE
                }).ToList());
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<ResourceAssignmentDto>>(
                    ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> UpdateResource(int id, AddResource request)
        {
            try
            {
                var resource = await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
                if (resource == null)
                {
                    return ApiResponseFactory.NotFound<string>("Resource not found");
                }

                _mapper.Map(request, resource);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success<string>("Resource updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while updating resource: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
