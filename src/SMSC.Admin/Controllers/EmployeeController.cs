using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SMSC.Application.Commands.Employee;
using SMSC.Application.DTO;
using SMSC.Application.Queries.Employee;
using SMSC.Core.Logger.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SMSC.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILog _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMediator _mediator;

        public EmployeeController(ILog logger, IStringLocalizer<SharedResources> localizer, IMediator mediator)
        {
            _logger = logger;
            _localizer = localizer;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddEmployee(EmployeeDto employeeDto, CancellationToken token)
        {
            var addEmployeeCommand = new AddEmployeeCommand()
            {
                EmployeeDto = employeeDto
            };

            var employeeResult = await _mediator.Send(addEmployeeCommand, token);
            return Json(employeeResult);
        }

        public async Task<JsonResult> GetEmployeeById(CancellationToken cancellationToken, int empIdentityCode)
        {
            var getEmployeeByIdQuery = new GetEmployeeByIdQuery
            {
                EmployeeId = empIdentityCode
            };

            var resultEmployeeDto = await _mediator.Send(getEmployeeByIdQuery, cancellationToken);
            return Json(resultEmployeeDto);
        }

        [HttpGet]
        public async Task<JsonResult> GetEmployeeList(CancellationToken cancellationToken, EmployeeDto employeeDto)
        {

            var getEmployeeListQuery = new GetEmployeeListQuery
            {
                EmployeeDto = employeeDto
            };

            var resultEmployeeDto = await _mediator.Send(getEmployeeListQuery, cancellationToken);
            return Json(resultEmployeeDto);
        }

        public async Task<JsonResult> UpdateEmployee(CancellationToken cancellationToken, EmployeeDto employeeDto)
        {

            var updateEmployeeCommand = new UpdateEmployeCommand
            {
                EmployeeDto = employeeDto
            };

            var employeeResult = await _mediator.Send(updateEmployeeCommand, cancellationToken);
            return Json(employeeResult);
        }

        public async Task<JsonResult> DeleteEmployeeById(CancellationToken cancellationToken, int employeeId)
        {

            var deleteEmployeeByIdCommand = new DeleteEmployeeByIdCommand
            {
                EmployeeId = employeeId
            };

            var resultEmployeeDto = await _mediator.Send(deleteEmployeeByIdCommand, cancellationToken);
            return Json(resultEmployeeDto);
        }

    }
}
