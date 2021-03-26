using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
   public class BuyDto
    {
        public int Amount { get; set; }
        public string CreditCardNumber { get; set; }
        public string SecurityNumber { get; set; }
        public string MounthOfExpirationDate { get; set; }
        public string YearOfExpirationDate { get; set; }
    }
}
