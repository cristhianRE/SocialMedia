using SocialMedia.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Model.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
