using SocialMedia.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}
