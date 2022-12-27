using Model.DAL;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class CommentRepository : ZooRepository<Comment>
    {
        public CommentRepository(ZooDBContext dbContext) : base(dbContext)
        {
        }
    }
}
