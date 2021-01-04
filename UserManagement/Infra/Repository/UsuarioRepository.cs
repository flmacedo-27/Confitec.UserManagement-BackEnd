using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Exceptions;
using UserManagement.Domain.Interfaces;
using UserManagement.Infra.Contexts;

namespace UserManagement.Infra.Repository
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private readonly DataContext _context;

        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAll()
        {
            try
            {
                return await Task.Run(() => _context.Usuarios.ToList());
            }
            catch (Exception)
            {
                throw new UsuarioException("Não foi possível recuperar as informações dos usuários. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }
        public async Task<Usuario> Get(Guid id)
        {
            try
            {
                return await Task.Run(() => _context.Usuarios.FirstOrDefault(x => x.Id == id));
            }
            catch (Exception)
            {
                throw new UsuarioException("Não foi possível localizar as informações do usuário. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }

        public async Task<bool> Insert(Usuario usuario)
        {
            try
            {
                await Task.Run(() => { _context.Usuarios.Add(usuario); });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                throw new UsuarioException("Não foi possível cadastrar o usuário. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }
        public async Task<bool> Update(Usuario usuario)
        {
            try
            {
                var currentUser = _context.Usuarios.FirstOrDefault(x => x.Id == usuario.Id);

                if (currentUser != null)
                {
                    currentUser.Nome = usuario.Nome;
                    currentUser.Sobrenome = usuario.Sobrenome;
                    currentUser.Email = usuario.Email;
                    currentUser.DataNascimento = usuario.DataNascimento;
                    currentUser.Escolaridade = usuario.Escolaridade;

                    await Task.Run(() => { _context.Usuarios.Update(currentUser); });
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw new UsuarioException("Não foi possível atualizar as informações do usuário. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var currentUser = _context.Usuarios.FirstOrDefault(x => x.Id == id);
                
                if (currentUser != null)
                {
                    await Task.Run(() =>
                    {
                        _context.Remove(currentUser);
                        _context.SaveChanges();
                    });
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw new UsuarioException("Não foi possível deletar as informações do usuário. Possível causa:\r\n -A conexão com a base de dados não foi estabelecida.");
            }
        }
    }
}
