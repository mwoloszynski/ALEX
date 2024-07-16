using COMMON.Models;
using DBMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Base
{
    public abstract class BaseRepository
    {
        protected AlexDbContext AlexDbContext()
        {
            AlexDbContext alexDbContext = new AlexDbContext(AppSettings.ConnectionString);
            return alexDbContext;
        }
    }
}
