using DAL.Repositories.Base;
using DBMODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ExchangeRepository<TEntity> : EntityRepository<TEntity> where TEntity : Exchange, new()
    {
        public ExchangeRepository()
        {

        }
    }
}
