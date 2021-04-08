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
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), AspectMessages.RentalListed);
        }

        public IDataResult<List<RentalDetailDto>> GetDetailsAll()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), AspectMessages.RentalListed);
        }

        public IDataResult<Rental> GetById(int Id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == Id), AspectMessages.RentalListed);
        }

        public IResult Add(Rental rental)
        {
            var result = RentalCarControl(rental.CarId);
            if (!result.SuccessStatus)
            {
                return new ErrorResult(AspectMessages.RentalNotDelivered);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(AspectMessages.RentalAdded);
        }

        public IResult RentalCarControl(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate != null).Any();//to be corrected
            if (result)
            {
                return new ErrorResult(AspectMessages.RentalNotDelivered);
            }

            return new SuccessResult(AspectMessages.RentalSuccess);
        }


        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(AspectMessages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(AspectMessages.RentalDeleted);
        }
    }
}

