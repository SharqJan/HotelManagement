using System;
namespace SMSC.Application.DTO
{

    public class InvoiceReportDto
    {
        
        public string ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalTax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal RoomCharges { get; set; }
        public int MinRevenue { get; set; }
        public int MaxRevenue { get; set; }


    }
}
