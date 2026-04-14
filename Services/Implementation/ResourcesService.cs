using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
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
                var resource = await _context.Resources.FirstOrDefaultAsync(r => r.Id == resourceId);
                if (resource == null)
                {
                    return ApiResponseFactory.NotFound<string>("Resource not found");
                }

                resource.ResourceAssignments.Add(new ResourceAssignment
                {
                    EmergencyEventId = eventId,
                    AssignedById = assignedById,
                    AssignedTime = DateTime.UtcNow
                });

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
                var resource = await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
                if (resource == null)
                {
                    return ApiResponseFactory.NotFound<string>("Resource not found");
                }

                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success<string>("Resource deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while deleting resource: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<ResourceDto>>> GetAvailableResources(RESOURCE_CATEGORY? category = null)
        {
            try
            {
                var resources = await _context.Resources
                .Where(r => r.Category == category)
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

        public async Task<ApiResponse<ResourceDto>> ReturnResource(int assignmentId)
        {
            try
            {
                var resources = await _context.Resources
               .Include(r => r.ResourceAssignments)
               .FirstOrDefaultAsync(r => r.ResourceAssignments.Any(a => a.Id == assignmentId));
                if (resources == null)
                {
                    return ApiResponseFactory.NotFound<ResourceDto>("Resource assignment not found");
                }

                var response = _mapper.Map<ResourceDto>(resources);
                return ApiResponseFactory.Success(response, "Resource returned successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<ResourceDto>("An error occurred while returning resource: " + ex.Message, HttpStatusCode.InternalServerError);
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
