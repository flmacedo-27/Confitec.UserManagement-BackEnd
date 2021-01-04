using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Exceptions;

namespace UserManagement.Domain.Behavior
{
    public class FailFastBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator> _validators;
        public FailFastBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationFailures = _validators
             .Select(validator => validator.Validate(context))
             .SelectMany(validationResult => validationResult.Errors)
             .Where(validationFailure => validationFailure != null)
             .ToList();

            if (validationFailures.Any())
            {
                var error = string.Join("\r\n", validationFailures);
                throw new UsuarioException(error);
            }

            return next();
        }
    }
}
