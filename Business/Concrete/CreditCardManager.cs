using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation.Validators;

namespace Business.Concrete
{
   public class CreditCardManager:ICreditCardService
    {
       ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        //[SecuredOperation("admin")]
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

      //  [ValidationAspect(typeof(CreditCardValidator),Priority = 1)]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(CreditCard entity)
        {
            _creditCardDal.Add(entity);
            return new SuccessResult();
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(CreditCard entity)
        {
            _creditCardDal.Delete(entity);
            return new SuccessResult();
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(CreditCard entity)
        {
            _creditCardDal.Update(entity);
            return new SuccessResult();
        }

        public IResult Buy(BuyDto buyDto)
        {
            var result = _creditCardDal.Buy(buyDto);
            if (result.SuccessStatus)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult(result.Message);
            }
        }

        public IResult Refund(BuyDto buyDto)
        {
            _creditCardDal.Refund(buyDto);
            return new SuccessResult();
        }
    }
}
