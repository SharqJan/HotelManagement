using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSC.Application.Commands.Employee
{
    public class DeleteEmployeeByIdCommand : IRequest<long>
    {
        public int EmployeeId { get; set; }
    }
}
