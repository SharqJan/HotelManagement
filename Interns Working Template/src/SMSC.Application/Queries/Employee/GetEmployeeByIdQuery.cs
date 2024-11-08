using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Queries.Employee
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int EmployeeId { get; set; }
    }
}
