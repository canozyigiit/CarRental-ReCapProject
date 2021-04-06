using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentACarContext>, ICustomerDal
    {
        public List<CustomerRentalDetailDto> GetRentalAndCustomerDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Customers
                    join u in context.Users
                        on c.UserId equals u.Id
                    join r in context.Rentals
                        on c.CustomerId equals r.CustomerId
                    select new CustomerRentalDetailDto
                    {
                        CustomerId = c.CustomerId,
                        UserId = u.Id,
                        RentalId = r.RentalId,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }

            public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
            {
                using (RentACarContext context = new RentACarContext())
                {
                    var result = from customer in filter is null ? context.Customers : context.Customers.Where(filter)

                        join user in context.Users on customer.UserId equals user.Id
                        select new CustomerDetailDto()
                        {
                            CustomerId = customer.CustomerId,
                            CompanyName = customer.CompanyName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            FindexScore = customer.FindexScore
                        };
                    return result.ToList();
                }
            }

            public bool DeleteCustomerIfNotReturnDateNull(Customer customer)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var find = context.Rentals.Any(i => i.CustomerId == customer.CustomerId && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(customer);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}