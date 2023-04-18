using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DAL
{
    public class BaseRepository<EntityType> : IBaseRepository<EntityType> where EntityType : BaseClass
    {
        private MyDbContext _dbContext;
        public BaseRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EntityType> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<EntityType>().FindAsync(id);
            return entity!;
        }

        public async Task<List<EntityType>> GetAllAsync()
        {
            var entities = await _dbContext.Set<EntityType>().ToListAsync();
            return entities;
        }

        public async Task<int> AddAsync(EntityType entity)
        {
            await _dbContext.Set<EntityType>().AddAsync(entity);
            _dbContext.Entry(typeof(EntityType)).State = EntityState.Detached;
            return entity.Id;
        }

        public async Task UpdateAsync(EntityType entity)
        {
            _dbContext.Set<EntityType>().Update(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(typeof(EntityType)).State = EntityState.Detached;
        }

        public async Task DeleteAsync(EntityType entity)
        {
            _dbContext.Set<EntityType>().Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

    }
}
