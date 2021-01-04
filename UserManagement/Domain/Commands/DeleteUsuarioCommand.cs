using MediatR;
using System;

namespace UserManagement.Domain.Commands
{
    public class DeleteUsuarioCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
