using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Azure.Web.Api.Models.Entities;
using src.Azure.Serverless.Api.DataLayer.Repositories;

namespace Azure.Web.Api.Business.Services
{
    public abstract class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly BaseRepository<TEntity> _repository;

        protected BaseService(BaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public IEnumerable<TEntity> GetAllAsync<T>(Expression<Func<TEntity, bool>> where,
                                                    Func<TEntity, T> orderBy,
                                                    bool orderAscending = true)
        {
            return _repository.GetAllByExpression<T>(where, orderBy, orderAscending);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<TEntity> GetByExpression(Expression<Func<TEntity, bool>> where)
        {
            return await _repository.GetByExpression(where);
        }
    }
}
