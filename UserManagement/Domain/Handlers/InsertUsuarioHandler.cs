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
    public class InsertUsuarioHandler : IRequestHandler<InsertUsuarioCommand, bool>
    {
        const string key = "Usuarios";
        private readonly IMediator _mediator;
        private readonly IRepository<Usuario> _repository;
        private readonly IMemoryCache _cache;

        public InsertUsuarioHandler(IMediator mediator, IRepository<Usuario> repository, IMemoryCache cache)
        {
            _mediator = mediator;
            _repository = repository;
            _cache = cache;
        }

        public async Task<bool> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario { Id = Guid.NewGuid(), Nome = request.Nome, Sobrenome = request.Sobrenome, Email = request.Email, DataNascimento = request.DataNascimento, Escolaridade = Convert.ToInt32(request.Escolaridade) };
            bool successful = await _repository.Insert(usuario);
            if (successful)
            {
                List<Usuario> collectionCache = null;
                if (_cache.TryGetValue(key, out collectionCache))
                    collectionCache.Add(usuario);
                else
                {
                    collectionCache = new List<Usuario>();
                    collectionCache.Add(usuario);
                }
                _cache.Set(key, collectionCache);

                await _mediator.Publish(new UsuarioCreatedNotification { Id = usuario.Id, Nome = usuario.Nome, Sobrenome = usuario.Sobrenome, Email = usuario.Email, DataNascimento = usuario.DataNascimento, Escolaridade = usuario.Escolaridade });
                return successful;
            }
            else
                return successful;
        }
    }
}
