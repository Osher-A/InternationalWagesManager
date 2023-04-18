using InternationalWagesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DAL
{
    public interface IBaseRepository<EntityType> where EntityType : BaseClass
    {
        Task<EntityType> GetByIdAsync(int id);
        Task<List<EntityType>> GetAllAsync();
        Task<int> AddAsync(EntityType entity);
        Task UpdateAsync(EntityType entity);
        Task DeleteAsync(EntityType entity);

    }
}
