using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Core.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDateTime { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string ReservationStatus { get; set; }
        public int GuestId { get; set; }   

    }
}
