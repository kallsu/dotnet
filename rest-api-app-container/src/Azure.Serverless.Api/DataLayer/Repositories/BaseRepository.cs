using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace src.Azure.Serverless.Api.DataLayer.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MyDbContext _dbContext;

        public BaseRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(long id)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> InsertAsync(TEntity newEntity)
        {
            newEntity.Created = DateTime.UtcNow;

            await _dbContext.Set<TEntity>().AddAsync(newEntity);
            await _dbContext.SaveChangesAsync();

            return newEntity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.Updated = DateTime.UtcNow;

            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}