using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.DTOs
{
   public class CustomerUpdateDto:IDto
    {
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public int FindexScore { get; set; }
        public int UserId { get; set; }
    }
}
