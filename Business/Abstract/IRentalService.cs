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

        IDataResult<List<RentalDetailDto>> GetDetailsAll();

        IDataResult<Rental> GetById(int Id);

        IResult Add(Rental rental);

        IResult RentalCarControl(int CarId);

        IResult Update(Rental rental);

        IResult Delete(Rental rental);
    }
}
