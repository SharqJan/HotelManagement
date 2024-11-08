using FluentResults;
using MediatR;
using SMSC.Application.DTO;
using System.Collections.Generic;


namespace SMSC.Application.Queries.Login
{
    public class LoginQuery : IRequest<Result<IEnumerable<LoginDto>>>
    {
        public LoginDto LoginDto { get; set; }
    }
}


