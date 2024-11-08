using FluentResults;
using MediatR;
using SMSC.Application.DTO;


namespace SMSC.Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<Result<UserDTO>>
    {
        public int UserId { get; set; }
    }
}
