﻿using System;

namespace SMSC.Application.DTO
{
    public class CheckOutDto
    {
        public int GuestId { get; set; }    
        public int RoomId { get; set; }
        public int RoomNo { get; set; }
        public int FloorNo { get; set; }
        public string RoomStatus { get; set; }
        public decimal RoomAdditionalTax { get; set; }
        public decimal RoomDefaultPrice { get; set; }

        public byte[] RoomImage1 { get; set; }
        public byte[] RoomImage2 { get; set; }
        public byte[] RoomImage3 { get; set; }
        public byte[] RoomImage4 { get; set; }
        public byte[] RoomImage5 { get; set; }
        public bool IsRoomAvailable { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }



    }
}