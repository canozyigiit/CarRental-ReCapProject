using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryBrandDal : IBrandDal
    {
        List<Brand> _brands;
        public InMemoryBrandDal()
        {
            _brands = new List<Brand>
            {
               
            };
        }
        public void Add(Brand entity)
        {
            _brands.Add(entity);    
        }

        public void Delete(Brand entity)
        {
            Brand colorToDelete = _brands.SingleOrDefault(b => b.BrandId == entity.BrandId);
            _brands.Remove(colorToDelete);
        }

        public Brand Get(Func<Brand, bool> filter = null)
        {
            return _brands.SingleOrDefault(filter);
        }


        public List<Brand> GetAll(Func<Brand, bool> filter = null)
        {
            return _brands.Where(filter).ToList();
        }

       
        public bool DeleteBrandIfNotReturnDateNull(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void Update(Brand entity)
        {
            Brand brandToUpdate = _brands.SingleOrDefault(b => b.BrandId == entity.BrandId);
            brandToUpdate.BrandId = entity.BrandId;
            brandToUpdate.BrandName = entity.BrandName;
        }
    }
}
