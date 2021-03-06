﻿using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetById(int customerId);
        IResult Add(Customer customer);
        IResult Delete(Customer customer);
        IResult Update(Customer customer);
        IResult EditCustomer(CustomerUpdateDto customer);
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
        IDataResult<List<CustomerDetailDto>> GetCustomerDetailByUserId(int userId);
        IDataResult<Customer> GetCustomerByUserId(int userId);
    }
}
