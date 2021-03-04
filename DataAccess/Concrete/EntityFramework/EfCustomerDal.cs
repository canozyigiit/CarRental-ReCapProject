using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from a in GetRentalAndCustomerDetails()
                    join c in context.Customers
                        on a.CustomerId equals c.CustomerId
                    select new CustomerDetailDto
                    {
                        CustomerId = a.CustomerId,
                        CompanyName = c.CompanyName
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