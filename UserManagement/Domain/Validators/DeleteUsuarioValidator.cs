using FluentValidation;
using UserManagement.Domain.Commands;

namespace UserManagement.Domain.Validators
{
    public class DeleteUsuarioValidator : AbstractValidator<DeleteUsuarioCommand>
    {
        public DeleteUsuarioValidator()
        {
            RuleFor(usuario => usuario.Id)
                .NotNull()
                .Length(36, 36).WithMessage("A UniqueKey deve conter exatamente 36 caracteres.")
                .Matches("[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")
                .NotEmpty().WithMessage("A UniqueKey deve ser informada para realizar a exclusão de um usuário.");
        }
    }
}
