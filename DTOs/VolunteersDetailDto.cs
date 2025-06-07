namespace EmergencyNotifRespons.DTOs
{
    public class VolunteersDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Skills { get; set; }
        public string AvailabilityStatus { get; set; }
        public string EmergencyContactPhone { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
    }
}
