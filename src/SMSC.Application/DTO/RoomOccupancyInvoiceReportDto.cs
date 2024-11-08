using System;
namespace SMSC.Application.DTO
{

    public class RoomOccupancyInvoiceReportDto
    {

        public string ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public decimal TotalRooms { get; set; }
        public decimal AvailableRooms { get; set; }
        public decimal OccupiedRooms { get; set; }
        public decimal OccupiedPercent { get; set; }
        public decimal NoOfCheckIns { get; set; }
        public decimal NoOfCheckOuts { get; set; }
        public decimal MinPercent { get; set; }
        public decimal MaxPercent { get; set; }


    }
}
