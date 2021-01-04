using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Domain.Exceptions
{
    public class UsuarioException : Exception
    {
        public UsuarioException(string message) : base(message) { }
    }
}
