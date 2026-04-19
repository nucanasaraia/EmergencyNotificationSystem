using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.Requests
{
    public class UpdateUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
