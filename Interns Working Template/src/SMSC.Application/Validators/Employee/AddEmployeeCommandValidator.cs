using FluentValidation;
using SMSC.Application.Commands.Employee;

namespace SMSC.Application.Validators.Employee
{
    public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
    {

        public AddEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeDto.Name).NotEmpty().MinimumLength(5);
        }
    }
}
