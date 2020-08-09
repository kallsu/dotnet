using System;
using System.Collections.Generic;
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

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<TEntity> CreateAsync<TModel>(TModel dto) where TModel : class
        {
            // use mapper to convert from model to entity

            return await _repository.InsertAsync(newEntity);
        }

        public async Task UpdateAsync<TModel>(TModel dto)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
