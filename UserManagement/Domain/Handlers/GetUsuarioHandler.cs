using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Domain.Handlers
{
    public class GetUsuarioHandler : IRequestHandler<GetUsuarioCommand, List<Usuario>>
    {
        const string key = "Usuarios";
        private readonly IMediator _mediator;
        private readonly IRepository<Usuario> _repository;
        private readonly IMemoryCache _cache;

        public GetUsuarioHandler(IMediator mediator, IRepository<Usuario> repository, IMemoryCache cache)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
        }

        public async Task<List<Usuario>> Handle(GetUsuarioCommand request, CancellationToken cancellationToken)
        {
            List<Usuario> resultCache = null;
            if (_cache.TryGetValue(key, out resultCache))
            {
                return await Task.FromResult(resultCache);
            }
            else
            {
                var result = await _repository.GetAll();
                if (result.Count > 0)
                    _cache.Set(key, result);

                return result;
            }
        }
    }
}
