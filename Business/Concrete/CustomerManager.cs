﻿using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
       // [SecuredOperation("admin")]
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerId == customerId));
        }

        [ValidationAspect(typeof(CustomerValidator), Priority =1)]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Added);
        }
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer)
        {
            var result = _customerDal.DeleteCustomerIfNotReturnDateNull(customer);
            if (result)
            {
                return new SuccessResult(Messages.Deleted);
            }

            return new ErrorResult(Messages.NotDeleted);

        }

        [ValidationAspect(typeof(CustomerValidator), Priority =1)]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Updated);
        }
        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetailByUserId(int userId)
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails(u => u.UserId == userId));
        }
        public IDataResult<Customer> GetCustomerByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.GetCustomerByUserId(u => u.UserId == userId));
        }
        public IResult EditCustomer(CustomerUpdateDto customer)
        {
            

            var customerInfo = new Customer()
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName,
                FindexScore = customer.FindexScore,
                UserId = customer.UserId
              
               
            };

            _customerDal.Update(customerInfo);
            return new SuccessResult(Messages.Updated);
        }
    }
}