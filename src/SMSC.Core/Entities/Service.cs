using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Core.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServiceCharges { get; set; }
        public bool IsActive { get; set; }
    }
}
