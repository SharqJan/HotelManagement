using AutoMapper;
using MediatR;
using SMSC.Application.Commands.Employee;
using SMSC.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Application.Handlers.Employee
{
    public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmployeeByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<long> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var resultEmployee = await _unitOfWork.EmployeeRepository.DeleteEmployeeByIdAsync(cancellationToken, request.EmployeeId);
            return resultEmployee;
        }
    }
}
