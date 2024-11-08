using FluentResults;
using MediatR;
using SMSC.Application.DTO;


namespace SMSC.Application.Queries.CheckOut
{
    public class GetInvoiceByRoomIdQuery : IRequest<Result<InvoiceDto>>
    {
        public int RoomId { get; set; }
    }
}
