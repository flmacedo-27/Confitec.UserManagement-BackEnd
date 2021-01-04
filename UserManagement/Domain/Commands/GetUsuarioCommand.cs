using MediatR;
using System.Collections.Generic;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Commands
{
    public class GetUsuarioCommand : IRequest<List<Usuario>> { }
}
