using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.DTOs
{
    public class EmergencyEventDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public EVENT_TYPE EventType { get; set; }
        public int Severity { get; set; }
        public string? Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal AffectedRadius { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ACTIVITY_STATUS ActivityStatus { get; set; }
        public string? CreatedByName { get; set; }
    }
}
