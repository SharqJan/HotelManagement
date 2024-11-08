using System;
namespace SMSC.Application.DTO
{

public class InvoiceDto
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public DateTime CheckoutDateTime { get; set; }
        public decimal SubTotal { get; set; }
        public string TaxName1 { get; set; }
        public decimal TaxPercentage1 { get; set; }
        public string TaxName2 { get; set; }
        public decimal TaxPercentage2 { get; set; }
        public string TaxName3 { get; set; }
        public decimal TaxPercentage3 { get; set; }
        public decimal AmountPaidInAdvance { get; set; }
        public decimal TotalAmount { get; set; }


     

}
}
