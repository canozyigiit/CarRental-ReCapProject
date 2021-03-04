using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, RentACarContext>, IUserDal
    {
        public List<CustomerRentalDetailDto> GetCustomerAndRentalDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Customers
                    join r in context.Rentals
                        on c.CustomerId equals r.CustomerId
                        join u in context.Users
                            on c.CustomerId equals  u.Id
                    select new CustomerRentalDetailDto()
                    {
                        RentalId = r.RentalId,
                        UserId = u.Id,
                        CustomerId = c.CustomerId,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate

                    };
                return result.ToList();

            }
        }

        public bool DeleteUserIfNotReturnDateNull(User user)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var find = GetCustomerAndRentalDetails().Any(i => i.UserId == user.Id && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(user);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new RentACarContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim 
                             { 
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();

            }
        }
    }
}