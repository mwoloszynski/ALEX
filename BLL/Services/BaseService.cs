using COMMON.Helpers;
using DAL.Repositories;
using DAL.Repositories.Base;
using DBMODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BaseService<TEntity> where TEntity : BaseEntity, new()
    {
        private EntityRepository<TEntity> _repository;

        public BaseService()
        {
            _repository = new EntityRepository<TEntity>();
        }

        public List<TEntity> GetAll(int? page = null, int? pageSize = null)
        {
            return _repository.GetAll().ToList();
        }

        public TEntity GetByCode(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.GetAll(1, 1, filter).FirstOrDefault();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetElement(id);
        }

        public async Task<Enums.Result> Add(TEntity entity)
        {
            return await _repository.Add(entity);
        }
    }
}
