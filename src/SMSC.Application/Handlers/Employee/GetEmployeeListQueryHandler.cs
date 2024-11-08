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
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var billing = await _unitOfWork.EmployeeRepository.GetEmployeeListAsync<EmployeeDto>(cancellationToken);
            return billing;
        }
    }
}