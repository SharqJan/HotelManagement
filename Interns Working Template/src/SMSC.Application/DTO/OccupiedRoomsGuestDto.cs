using System;


namespace SMSC.Application.DTO
{
    public class OccupiedRoomsGuestDto
    {
        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}


