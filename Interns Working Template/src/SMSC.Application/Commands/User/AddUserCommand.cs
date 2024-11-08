
using FluentResults;
using MediatR;
using SMSC.Application.DTO;

namespace SMSC.Application.Commands.User
{
    public class AddUserCommand : IRequest<Result<long>>
    {
        public UserDTO UserDTO { get; set; }

    }
}
