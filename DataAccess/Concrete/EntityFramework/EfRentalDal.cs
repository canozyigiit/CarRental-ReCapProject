using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join r in context.Rentals
                             on c.CarId equals r.CarId
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join cstmr in context.Customers
                             on r.CustomerId equals cstmr.CustomerId
                             join u in context.Users
                             on cstmr.UserId equals u.Id
                             select new RentalDetailDto 
                             {
                                  RentalId = r.RentalId,
                                  CarName = b.BrandName,
                                  CustomerName = u.FirstName,
                                  CustomerLastName= u.LastName,
                                  CompanyName= cstmr.CompanyName,
                                  RentDate = r.RentDate,
                                  ReturnDate=r.ReturnDate
                                                              
                             };
                return result.ToList();

            }
        }
        
        
        public bool DeleteRentalIfNotReturnDateNull(Rental rental)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var find = context.Rentals.Any(i => i.RentalId == rental.RentalId && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(rental);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            
        }
    }
}
