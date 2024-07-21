using COMMON.Helpers;
using COMMON.Helpers.Extensions;
using DBMODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Base
{
    public class EntityRepository<TEntity> : BaseRepository where TEntity : BaseEntity, new()
    {
        public virtual IEnumerable<TEntity> GetAll(int? page = null, int? pageSize = null, Expression<Func<TEntity, bool>> filter = null)
        {
            using(var context = AlexDbContext())
            {
                var result = context.Set<TEntity>().AsQueryable();
                return filter == null ? result.ToPaged(page, pageSize).ToList() : result.Where(filter).ToPaged(page, pageSize).ToList();
            }
        }

        public virtual TEntity? GetElement(int id)
        {
            using (var context = AlexDbContext())
            {
                return context.Set<TEntity>().Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public async virtual Task<Enums.Result> Add(TEntity entity)
        {
            try
            {
                using(var context = AlexDbContext())
                {
                    context.Set<TEntity>().Add(entity);
                    await context.SaveChangesAsync();
                }
                return Enums.Result.Success;
            }
            catch (Exception ex)
            {
                return Enums.Result.Fail;
            }
        }

        public async virtual Task<Enums.Result> Update(TEntity entity)
        {
            try
            {
                using(var context = AlexDbContext())
                {
                    await context.Entry(entity).ReloadAsync();

                    context.Set<TEntity>().Update(entity);
                    await context.SaveChangesAsync();
                }
                return Enums.Result.Success;
            }
            catch(Exception ex)
            {
                return Enums.Result.Fail;
            }
        }

        public async virtual Task<Enums.Result> Delete(TEntity entity)
        {
            try
            {
                using (var context = AlexDbContext())
                {
                    await context.Entry(entity).ReloadAsync();

                    context.Set<TEntity>().Remove(entity);
                    await context.SaveChangesAsync();
                }
                return Enums.Result.Success;
            }
            catch (Exception ex)
            {
                return Enums.Result.Fail;
            }
        }
    }
}
