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
    public class UpdateUsuarioHandler : IRequestHandler<UpdateUsuarioCommand, bool>
    {
        const string key = "Usuarios";
        private readonly IMediator _mediator;
        private readonly IRepository<Usuario> _repository;
        private readonly IMemoryCache _cache;

        public UpdateUsuarioHandler(IMediator mediator, IRepository<Usuario> repository, IMemoryCache cache)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
        }
        public async Task<bool> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario { Id = new Guid(request.Id), Nome = request.Nome, Sobrenome = request.Sobrenome, Email = request.Email, DataNascimento = request.DataNascimento, Escolaridade = request.Escolaridade };
            bool successful = await _repository.Update(usuario);
            if (successful)
            {
                List<Usuario> collectionCache = null;
                if (_cache.TryGetValue(key, out collectionCache))
                {
                    collectionCache.Remove(collectionCache.Find(x => x.Id == usuario.Id));
                    collectionCache.Add(usuario);
                }
                else
                {
                    collectionCache = new List<Usuario>();
                    collectionCache.Add(usuario);
                }
                _cache.Set(key, collectionCache);

                await _mediator.Publish(new UsuarioUpdatedNotification { Id = usuario.Id, Nome = usuario.Nome, Sobrenome = usuario.Sobrenome, Email = usuario.Email, DataNascimento = usuario.DataNascimento, Escolaridade = usuario.Escolaridade });
                return successful;
            }
            else
                return successful;
        }
    }
}
