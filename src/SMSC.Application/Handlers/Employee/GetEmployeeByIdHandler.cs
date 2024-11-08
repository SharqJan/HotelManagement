using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Interfaces;
using SMSC.Application.Queries.Employee;
using SMSC.Core.Interfaces;
using SMSC.Core.Entities;
using SMSC.Application.DTO;

namespace SMSC.Application.Handlers.Employee
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employeeId = request.EmployeeId;
            var resultEmployee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(cancellationToken, employeeId);
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(resultEmployee);
            return employeeDto;
        }
    }
}