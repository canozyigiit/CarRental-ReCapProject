using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal : EfEntityRepositoryBase<Brand, RentACarContext> , IBrandDal
    {
        public List<CarBrandDetailDto> GetCarAndBrandDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join r in context.Rentals
                             on c.CarId equals r.CarId
                             select new CarBrandDetailDto
                             {
                                 CarId = c.CarId,
                                 BrandId = b.BrandId,
                                 RentalId = r.RentalId,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };

                return result.ToList();

            }
        }
        
        public bool DeleteBrandIfNotReturnDateNull(Brand brand)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var find = GetCarAndBrandDetails().Any(i => i.BrandId == brand.BrandId && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(brand);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
