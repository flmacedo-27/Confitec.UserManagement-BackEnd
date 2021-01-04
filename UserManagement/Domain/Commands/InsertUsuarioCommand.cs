using MediatR;
using System;

namespace UserManagement.Domain.Commands
{
    public class InsertUsuarioCommand : IRequest<bool>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Escolaridade { get; set; }
    }
}
