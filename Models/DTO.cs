namespace LoveSyncServer.Models
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public bool? Theme { get; set; }
        public bool? NotificationsEnabled { get; set; }
    }
    public class CreateCoupleRequest
    {
        public int UserId { get; set; }
    }

    public class JoinCoupleRequest
    {
        public int UserId { get; set; }
        public string InviteCode { get; set; }
    }
    public class LeaveCoupleRequest
    {
        public int UserId { get; set; }
    }



}
