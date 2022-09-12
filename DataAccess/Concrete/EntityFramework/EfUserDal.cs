using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetOperationClaims(User user)
        {
            using (var context = new NorthwindContext())
            {
                var result = from operationCalim in context.OperationClaims
                             join userOperationCalim in context.UserOperationClaims
                             on operationCalim.Id equals userOperationCalim.Id
                             where userOperationCalim.UserId == user.Id
                             select new OperationClaim { Id = operationCalim.Id, Name = operationCalim.Name };
                return result.ToList();
            }
        }
    }
}
