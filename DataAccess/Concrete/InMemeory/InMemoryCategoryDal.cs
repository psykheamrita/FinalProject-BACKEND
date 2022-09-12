using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemeory
{
    public class InMemoryCategoryDal : ICategoryDal
    {
        public void Add(Category enity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category enity)
        {
            throw new NotImplementedException();
        }

        public Category Get(Expression<Func<Category, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Category enity)
        {
            throw new NotImplementedException();
        }
    }
}
