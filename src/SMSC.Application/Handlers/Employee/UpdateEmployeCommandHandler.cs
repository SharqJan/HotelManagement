using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Commands.Employee;
using SMSC.Application.DTO;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Employee
{
    class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<string> Handle(UpdateEmployeCommand request, CancellationToken cancellationToken)
        {
            EmployeeDto employeeDto = new EmployeeDto()
            {
               EmployeeId = request.EmployeeId,
                
            };

            var employee = _mapper.Map<Core.Entities.Employee>(employeeDto);

            long resultEmployeeId = await _unitOfWork.EmployeeRepository.UpdateEmployeeAsync(cancellationToken, employee);

            return "OK : Employee ID = " + resultEmployeeId.ToString();

        }
    }
}
