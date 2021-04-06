using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserCreditCardManager : IUserCreditCardService
    {
        private IUserCreditCardDal _userCreditCardDal;

        public UserCreditCardManager(IUserCreditCardDal userCreditCardDal)
        {
            _userCreditCardDal = userCreditCardDal;
        }

        public IResult Add(UserCreditCard card)
        {

            _userCreditCardDal.Add(card);
            return new SuccessResult(AspectMessages.UserCreditCardAddSuccess);
        }

        

        public IDataResult<UserCreditCard> GetByUserId(int userId)
        {
            return new SuccessDataResult<UserCreditCard>(_userCreditCardDal.Get(c => c.Id == userId));
        }

        public IDataResult<List<UserCreditCard>> GetAll(int userId)
        {
            return new SuccessDataResult<List<UserCreditCard>>(_userCreditCardDal.GetAll(x => x.UserId == userId), AspectMessages.CustomerCreditCardsListed);

           
        }
    }
}
