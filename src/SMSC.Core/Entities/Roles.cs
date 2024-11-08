
namespace SMSC.Core.Entities
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int CreatedBy { get; set; }  
        public bool IsActive { get; set; }
    }
}
