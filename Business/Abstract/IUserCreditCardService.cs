using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserCreditCardService
    {
        IResult Add(UserCreditCard card);
       
        IDataResult<UserCreditCard> GetByUserId(int id);
        IDataResult<List<UserCreditCard>> GetAll(int userId);
    }
}
