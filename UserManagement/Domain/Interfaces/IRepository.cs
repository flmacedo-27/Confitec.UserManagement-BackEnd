using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
        Task<bool> Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(Guid id);
    }
}
