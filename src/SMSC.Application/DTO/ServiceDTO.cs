
using System;

namespace SMSC.Application.DTO
{

    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public decimal ServiceCharges { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}


