using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int carId);
        IDataResult<List<Rental>> GetRentalByUndelivered();
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IResult Add(Rental rental);
        IDataResult<List<Rental>> GetByCarId(int carId);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);
        IResult IsRentable(Rental rental);


    }
}
