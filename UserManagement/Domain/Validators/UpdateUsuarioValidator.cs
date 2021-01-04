using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Commands;

namespace UserManagement.Domain.Validators
{
    public class UpdateUsuarioValidator : AbstractValidator<UpdateUsuarioCommand>
    {
        public UpdateUsuarioValidator()
        {
            RuleFor(usuario => usuario.Id)
               .NotNull()
              .Length(36, 36).WithMessage("A UniqueKey deve conter exatamente 36 caracteres.")
               .Matches("[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?")
               .NotEmpty().WithMessage("A UniqueKey deve ser informada para atualizar um usuário.");

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
