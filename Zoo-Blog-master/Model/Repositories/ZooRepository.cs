using Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class ZooRepository<T> : RepositoryBase<T,ZooDBContext> where T:class
    {
        public ZooRepository(ZooDBContext dbContext) : base(dbContext)
        {
        }
    }
}
