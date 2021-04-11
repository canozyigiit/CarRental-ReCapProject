using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Messages = Business.Constans.Messages;

namespace Business.Concrete
{
    public class FindeksManager : IFindeksService
    {
        private ICarService _carService;
        private ICustomerService _customerService;

        public FindeksManager(ICustomerService customerService, ICarService carService)
        {
            _customerService = customerService;
            _carService = carService;
        }


        public IResult Check(int carId, int customerId)
        {
            Thread.Sleep(2000);
            int carFindexScore = _carService.GetById(carId).Data.FindexScore;
            int customerFindexScore = _customerService.GetById(customerId).Data.FindexScore;
            if (carFindexScore > customerFindexScore)
            {
                return new ErrorResult(Messages.FindexScoreError);
            }
            return new SuccessResult(Messages.FindexScoreSuccess);
        }
    }
}
