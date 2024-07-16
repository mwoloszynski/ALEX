using COMMON.Helpers;
using DAL.Repositories.Base;
using DBMODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StockResultRepository<TEntity> : EntityRepository<TEntity> where TEntity : StockResult, new()
    {
        public StockResultRepository()
        {

        }
    }
}
