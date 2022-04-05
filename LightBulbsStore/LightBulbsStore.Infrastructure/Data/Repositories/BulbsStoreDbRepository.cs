using LightBulbsStore.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBulbsStore.Infrastructure.Data.Repositories
{
    public class BulbsStoreDbRepository : Repository, IBulbsStoreDbRepository
    {
        public BulbsStoreDbRepository(BulbsStoreDbContext context)
        {
            this.Context = context;
        }
    }
}
