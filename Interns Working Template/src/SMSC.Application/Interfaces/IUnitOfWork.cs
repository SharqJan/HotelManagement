namespace SMSC.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IRoomRepository RoomRepository { get; }
        IGuestRepository GuestRepository { get; }
        IReservationRepository ReservationRepository { get; }
        ICheckInCheckOutRepository CheckInCheckOutRepository { get; }
        IInvoiceReportRepository InvoiceReportRepository { get; }
        IRoomOccupancyInvoiceReportRepository RoomOccupancyInvoiceReportRepository { get; }
        IServiceUsageReportRepository ServiceUsageReportRepository { get; }
        ILoginRepository LoginRepository { get; }



    }
}
