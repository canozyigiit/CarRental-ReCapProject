using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        private IBrandService _brandService;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        //[SecuredOperation("admin")]
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c=> c.CarId == carId), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=> c.ColorId == colorId), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return  new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.Listed);
        }
       // [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator), Priority =1)]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult  Add(Car car)
        {
           // IResult result = BusinessRules.Run(CheckIfCarDescriptionExists(car.Description), CheckIfBrandLimitExceded());
            
            _carDal.Add(car);
            return new SuccessResult(Messages.Added);
            
        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {

            _carDal.Delete(car);
            return new SuccessResult(Messages.Deleted);
            
        }


        [ValidationAspect(typeof(CarValidator), Priority =1)]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            _carDal.Add(car);
            _carDal.Update(car);
            _carDal.Delete(car);
            return new SuccessResult(Messages.Updated);
        }
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorAndByBrand(int colorId, int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId && c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCar(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.CarId == carId));
        }
        private IResult CheckIfCarDescriptionExists(string description)
        {
            var result = _carDal.GetAll(c => c.Description == description).Any();
            if (result)
            {
                return new ErrorResult(Constans.Messages.DescriptionAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll();
            if ( result.Data.Count> 5)
            {
                return new ErrorResult(Constans.Messages.BrandLimitExceded);
            }

            return new SuccessResult();
        }
    }
}
