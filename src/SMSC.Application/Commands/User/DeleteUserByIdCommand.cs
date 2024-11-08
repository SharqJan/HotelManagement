using FluentResults;
using MediatR;

namespace SMSC.Application.Commands.User
{
    public class DeleteUserByIdCommand : IRequest<Result<long>>
    {
        public int UserId { get; set; }
    }
}
