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
using Messages = Business.Constans.Messages;

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
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<List<RentalDetailDto>> GetDetailsAll()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetById(int Id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == Id), Messages.RentalListed);
        }

        public IResult Add(Rental rental)
        {
            var result = RentalCarControl(rental.CarId);
            if (!result.SuccessStatus)
            {
                return new ErrorResult(Messages.RentalNotDelivered);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult RentalCarControl(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate != null);//to be corrected
            if (result.Count > 0) return new ErrorResult(Messages.RentalNotDelivered);

            return new SuccessResult(Messages.RentalSuccess);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult IsRentable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (result.Any(r =>
                r.ReturnDate >= rental.RentDate &&
                r.ReturnDate <= rental.ReturnDate
            )) return new ErrorResult(Messages.RentalNotDelivered);

            return new SuccessResult();
        }
     

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }
    }
}

