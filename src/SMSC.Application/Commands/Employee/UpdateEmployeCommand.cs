using MediatR;
using SMSC.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Employee
{
    public  class UpdateEmployeCommand : IRequest<string>
    {
        public EmployeeDto EmployeeDto { get; set; }
        public int EmployeeId { get; set; }

    }
}
