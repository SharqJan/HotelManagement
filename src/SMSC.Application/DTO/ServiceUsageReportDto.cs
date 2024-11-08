using System;
namespace SMSC.Application.DTO
{

    public class ServiceUsageReportDto
    {

        public string ServiceName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public decimal ServiceRevenue { get; set; }
        public decimal AverageSpent { get; set; }
        public int NoOfGuests {  get; set; }    
        


    }
}
