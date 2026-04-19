using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? FirstName { get; set; }      //filled later via UpdateUser
    public string? LastName { get; set; }      
    public string? PhoneNumber { get; set; }   
    public string? Location { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public ROLES_TYPE Role { get; set; } = ROLES_TYPE.CITIZEN;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool EmailVerified { get; set; }
    public string? VerificationCode { get; set; }
    public int VerificationAttempts { get; set; }
    public DateTime? VerificationCodeExpires { get; set; }
    public string? PasswordResetTokenHash { get; set; }
    public DateTime? PasswordResetTokenExpires { get; set; }
    public ICollection<EmergencyEvent>? EmergencyEvents { get; set; }
    public ICollection<UserNotification>? UserNotifications { get; set; }
    public ICollection<ResourceAssignment>? ResourceAssignments { get; set; }
    public ICollection<VolunteerAssignment>? VolunteerAssignments { get; set; }
    public Volunteer? VolunteerProfile { get; set; }
}