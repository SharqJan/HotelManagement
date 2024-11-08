using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SMSC.Application.Commands.Employee;
using SMSC.Application.Interfaces;

namespace SMSC.Application.Handlers.Employee
{
    class AddEmployeeHandler :  IRequestHandler<AddEmployeeCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public AddEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

      
        public async Task<string> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Core.Entities.Employee>(request.EmployeeDto);
            
            var resultEmployeeId =  await _unitOfWork.EmployeeRepository.AddEmployeeAsync(cancellationToken, employee);

            return "OK : Employee ID = " + resultEmployeeId;

        }
    }
}
