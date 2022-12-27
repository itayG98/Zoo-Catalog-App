using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.DAL;
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class AnimalRepository : ZooRepository<Animal>
    {
        public AnimalRepository(ZooDBContext dbContext) : base(dbContext)
        {
        }

        public  List<Animal> FindTopTrending(int count)
            => FindAllWithComments().OrderByDescending(c => c.Comments?.Count).Take(count).ToList();

        public IEnumerable<Animal> FindAllWithComments()
            => DbContext.Set<Animal>()!.Include(a => a.Comments).AsEnumerable();

        public Animal? FindWithComments(Guid id)
         => DbContext.Set<Animal>().Include(a => a.Comments).FirstOrDefault(a => a.ID == id);
    }
}
