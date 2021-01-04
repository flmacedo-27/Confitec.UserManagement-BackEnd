using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Notifications;

namespace UserManagement.Domain.EventHandlers
{
    public class LogEventHandler : INotificationHandler<UsuarioCreatedNotification>, INotificationHandler<UsuarioUpdatedNotification>, INotificationHandler<UsuarioDeletedNotification>
    {
        public Task Handle(UsuarioCreatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine($"CRIACAO: 'Id: {notification.Id} - Nome: {notification.Nome} - Sobrenome: {notification.Sobrenome} - Email: {notification.Email} - Data de Nascimento: {notification.DataNascimento} - Escolaridade: {notification.Escolaridade}"); });
        }

        public Task Handle(UsuarioUpdatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine($"ATUALIZACAO: 'Id: {notification.Id} - Nome: {notification.Nome} - Sobrenome: {notification.Sobrenome} - Email: {notification.Email} - Data de Nascimento: {notification.DataNascimento} - Escolaridade: {notification.Escolaridade}"); });
        }

        public Task Handle(UsuarioDeletedNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine($"EXCLUSAO: 'Id: {notification.Id} - Nome: {notification.Nome} - Sobrenome: {notification.Sobrenome} - Email: {notification.Email} - Data de Nascimento: {notification.DataNascimento} - Escolaridade: {notification.Escolaridade}"); });
        }
    }
}
