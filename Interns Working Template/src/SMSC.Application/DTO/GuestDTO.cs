using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.DTO
{
    public class GuestDTO
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public int ParentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public byte[] IdCard { get; set; }
        public string IdCardType { get; set; }
        public bool IsActive { get; set; }
        public bool AssignParent { get; set; }
        public int CreatedBy  { get; set; }
        //
        public string ServiceIds { get; set; }
        public DateTime CheckOutDate {  get; set; }
    }
}
