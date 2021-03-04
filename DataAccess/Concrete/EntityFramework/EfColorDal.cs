using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfColorDal : EfEntityRepositoryBase<Color, RentACarContext>, IColorDal
    {
        public List<CarColorDetailDto> GetCarColorDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                    join co in context.Colors
                        on c.ColorId equals co.ColorId
                    join r in context.Rentals
                        on c.CarId equals r.CarId
                    select new CarColorDetailDto()
                    {
                        ColorId = co.ColorId,
                        CarId = c.CarId,
                        RentalId = r.RentalId,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }
        
        public bool DeleteColorIfNotReturnDateNull(Color color)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var find = GetCarColorDetails().Any(i => i.ColorId == color.ColorId && i.ReturnDate == null);
                if (!find)
                {
                    context.Remove(color);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
