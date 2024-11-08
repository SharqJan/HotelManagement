using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Queries.Employee
{
    public class GetEmployeeListQuery : IRequest<IEnumerable<EmployeeDto>>
    {
        public DTO.EmployeeDto EmployeeDto { get; set; }
    }
}