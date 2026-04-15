namespace EmergencyNotifRespons.DTOs
{
    public class VolunteerAssignmentDto
    {
        public int Id { get; set; }
        public int EmergencyEventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime AssignedTime { get; set; }
        public DateTime? CompletedTime { get; set; }
    }
}
