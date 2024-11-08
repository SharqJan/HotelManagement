#nullable enable
namespace SMSC.Core.Entities
{
    public class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public byte[]? ProfileImage { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
