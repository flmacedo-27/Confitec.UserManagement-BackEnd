using MediatR;
using System;

namespace UserManagement.Domain.Commands
{
    public class UpdateUsuarioCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Escolaridade { get; set; }
    }
}
