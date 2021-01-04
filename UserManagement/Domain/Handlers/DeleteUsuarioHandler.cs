using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Domain.Notifications;

namespace UserManagement.Domain.Handlers
{
    public class DeleteUsuarioHandler : IRequestHandler<DeleteUsuarioCommand, bool>
    {
        const string key = "Usuarios";
        private readonly IMediator _mediator;
        private readonly IRepository<Usuario> _repository;
        private readonly IMemoryCache _cache;

        public DeleteUsuarioHandler(IMediator mediator, IRepository<Usuario> repository, IMemoryCache cache)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
        }
        public async Task<bool> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var guid = new Guid(request.Id);
            bool successful = await _repository.Delete(guid);
            if (successful)
            {
                List<Usuario> collectionCache = null;
                if (_cache.TryGetValue(key, out collectionCache))
                {
                    collectionCache.Remove(collectionCache.Find(x => x.Id == guid));
                    _cache.Set(key, collectionCache);
                }

                await _mediator.Publish(new UsuarioDeletedNotification { Id = guid });
                return successful;
            }
            else
            {
                return successful;
            }
        }
    }
}
