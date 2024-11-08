using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.DTO
{
   public class ReservationDto
    {
        public int GuestId { get; set; }
        public int ReservationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public DateTime ReservationDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal AdvanceAmount {  get; set; } 
        public string ReservationStatus { get; set; }
        public bool IsActive { get; set; }
        public string AssignParent { get; set; }
        public int CreatedBy { get; set; }
    }
}
