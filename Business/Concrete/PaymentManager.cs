using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Messages = Business.Constans.Messages;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
       
        public PaymentManager( )
        {
        }

        public IResult Payment(UserCreditCard card)
        { 
            return new SuccessResult(Messages.PaymentSuccess);
        }
    }
}
