using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


        public IEnumerable<TEntity> GetAllByExpression<T>(Expression<Func<TEntity, bool>> where,
                                                    Func<TEntity, T> orderBy,
                                                    bool orderAscending = true)
        {
            if (!orderAscending)
            {
                return _dbContext.Set<TEntity>().Where(where)
                                            .OrderByDescending(orderBy)
                                            .ToList();
            }

            return _dbContext.Set<TEntity>().Where(where)
                                            .OrderByDescending(orderBy)
                                            .ToList();
        }

        public async Task<TEntity> GetById(long id)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> GetByExpression(Expression<Func<TEntity, bool>> where)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(where);
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