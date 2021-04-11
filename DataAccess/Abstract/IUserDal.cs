using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal :IEntityRepository<User>
    {
       
        List<CustomerRentalDetailDto> GetCustomerAndRentalDetails();
        bool DeleteUserIfNotReturnDateNull(User user);
        List<OperationClaim> GetClaims(User user);
        List<OperationClaim> GetClaimsByUserId(Expression<Func<User, bool>> filter = null);



    }
}
