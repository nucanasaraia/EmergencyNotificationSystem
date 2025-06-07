namespace EmergencyNotifRespons.Requests
{
    public class AddEmergencyEvent
    {
        public int CreatedById { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public int Severity { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal AffectedRadius { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
