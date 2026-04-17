using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.DTOs
{
    public class ResourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public RESOURCE_CATEGORY Category { get; set; }
        public RESOURCE_STATUS Status { get; set; }
    }
}
