using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            
        }

        private static void UserAndCustomerTest()
        {
          

        }


        private static void BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            foreach (var brand in brandManager.GetAll().Data)
            {
              ;
            }
        }

        private static void ColorTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.ColorName);
            }
            Console.WriteLine();
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine("CarID: {0}\nBrandName: {1}\nBrandModel: {2}\nColorName: {3}\nDailyPrice: {4}\nDescription: {5}\n",
                    car.CarId, car.BrandName, car.BrandModel, car.ColorName, car.DailyPrice, car.Description);
            }

        }
    }
}
