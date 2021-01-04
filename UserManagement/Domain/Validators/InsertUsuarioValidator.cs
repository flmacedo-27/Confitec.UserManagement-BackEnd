using FluentValidation;
using UserManagement.Domain.Commands;

namespace UserManagement.Domain.Validators
{
    public class InsertUsuarioValidator : AbstractValidator<InsertUsuarioCommand>
    {
        public InsertUsuarioValidator()
        {
            RuleFor(usuario => usuario.Nome)
                .NotNull()
                .NotEmpty().WithMessage("O nome é obrigatório");

            RuleFor(usuario => usuario.Sobrenome)
               .NotNull()
               .NotEmpty().WithMessage("O sobrenome é obrigatório");

            RuleFor(usuario => usuario.Email)
                .NotNull()
                .NotEmpty().WithMessage("O e-mail é obrigatório");

            RuleFor(usuario => usuario.DataNascimento)
                .NotNull()
                .NotEmpty().WithMessage("A data de nascimento é obrigatória");

            RuleFor(usuario => usuario.Escolaridade)
                .NotNull()
                .NotEmpty().WithMessage("A escolaridade é obrigatória.");
        }
    }
}
