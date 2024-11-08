using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SMSC.Application.DTO;

namespace SMSC.Application.Commands.Employee
{
    public class AddEmployeeCommand : IRequest<string>
    {
        public EmployeeDto EmployeeDto { get; set; }
       
    }
}
