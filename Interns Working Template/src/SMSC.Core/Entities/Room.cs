using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Core.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomNo { get; set; }
        public int FloorNo { get; set; }
        public decimal RoomDefaultPrice { get; set; }
        public decimal RoomAdditionalTax { get; set; }
        public string RoomStatus { get; set; }
        public List<byte[]> RoomImages { get; set; } = new List<byte[]>();
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
