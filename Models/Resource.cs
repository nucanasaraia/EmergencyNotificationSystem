using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public RESOURCE_CATEGORY Category { get; set; } = RESOURCE_CATEGORY.PERSONNEL;

        public STATUS Status { get; set; } = STATUS.NONE;

        public ICollection<ResourceAssignment> ResourceAssignments { get; set; }
    }
}
