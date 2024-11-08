using SMSC.Application.Interfaces;
using SMSC.Core.Interfaces;

namespace SMSC.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //Define Data Access Repositories Here
        private readonly IRepository _dbRepository;

        public UnitOfWork(IRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }



        //Define All Repositories Interfaces Here
        private IEmployeeRepository _employeeRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IServiceRepository _serviceRepository;
        private IRoomRepository _roomRepository;
        private IGuestRepository _guestRepository;
        private IReservationRepository _reservationRepository;
        private ICheckInCheckOutRepository _checkInCheckOutRepository;
        private IInvoiceReportRepository _invoiceReportRepository;
        private IRoomOccupancyInvoiceReportRepository _roomOccupancyInvoiceReportRepository;
        private IServiceUsageReportRepository _serviceUsageReportRepository;
        private ILoginRepository _loginRepository;
        // Initialize All Interfaces Here
        public IEmployeeRepository EmployeeRepository { get { _employeeRepository = (_employeeRepository == null) ? new EmployeeRepository(_dbRepository) : _employeeRepository; return _employeeRepository; } }
        public IRoleRepository RoleRepository { get { _roleRepository = (_roleRepository == null) ? new RoleRepository(_dbRepository) : _roleRepository; return _roleRepository; } }
        public IUserRepository UserRepository { get { _userRepository = (_userRepository == null) ? new UserRepository(_dbRepository) : _userRepository; return _userRepository; } }
        public IServiceRepository ServiceRepository { get { _serviceRepository = (_serviceRepository == null) ? new ServiceRepository(_dbRepository) : _serviceRepository; return _serviceRepository; } }
        public IRoomRepository RoomRepository { get { _roomRepository = (_roomRepository == null) ? new RoomRepository(_dbRepository) : _roomRepository; return _roomRepository; } }
        public IGuestRepository GuestRepository { get { _guestRepository = (_guestRepository == null) ? new GuestRepository(_dbRepository) : _guestRepository; return _guestRepository; } }
        public IReservationRepository ReservationRepository { get { _reservationRepository = (_reservationRepository == null) ? new ReservationRepository(_dbRepository) : _reservationRepository; return _reservationRepository; } }
        public ICheckInCheckOutRepository CheckInCheckOutRepository { get { _checkInCheckOutRepository = (_checkInCheckOutRepository == null) ? new CheckInCheckOutRepository(_dbRepository) : _checkInCheckOutRepository; return _checkInCheckOutRepository; } }
        public IInvoiceReportRepository InvoiceReportRepository { get { _invoiceReportRepository = (_invoiceReportRepository == null) ? new InvoiceReportRepository(_dbRepository) : _invoiceReportRepository; return _invoiceReportRepository; } }
        public IRoomOccupancyInvoiceReportRepository RoomOccupancyInvoiceReportRepository { get { _roomOccupancyInvoiceReportRepository = (_roomOccupancyInvoiceReportRepository == null) ? new RoomOccupancyInvoiceReportRepository(_dbRepository) : _roomOccupancyInvoiceReportRepository; return _roomOccupancyInvoiceReportRepository; } }
        public IServiceUsageReportRepository ServiceUsageReportRepository { get { _serviceUsageReportRepository = (_serviceUsageReportRepository == null) ? new ServiceUsageReportRepository(_dbRepository) : _serviceUsageReportRepository; return _serviceUsageReportRepository; } }
        public ILoginRepository LoginRepository { get { _loginRepository = (_loginRepository == null) ? new LoginRepository(_dbRepository) : _loginRepository; return _loginRepository; } }

    }
}
