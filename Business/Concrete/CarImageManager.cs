using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Uploads.ImageUploads;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _imageDal;

        public CarImageManager(ICarImageDal imageDal)
        {
            _imageDal = imageDal;
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        [SecuredOperation("admin")]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_imageDal.GetAll(), Messages.Listed);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            //return new SuccessDataResult<List<CarImage>>(_imageDal.GetAll(i => i.CarID == carId), Messages.Listed);

            var result = _imageDal.GetAll(i => i.CarId == carId);

            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarImage>>(result);
            }

            List<CarImage> images = new List<CarImage>();
            images.Add(new CarImage() { CarId = 0, ImageId = 0, Date = DateTime.Now, ImagePath = "/images/car-rent.png" });

            return new SuccessDataResult<List<CarImage>>(images);
        }

        [CacheAspect]
        public IDataResult<CarImage> GetById(int imageId)
        {
            return new SuccessDataResult<CarImage>(_imageDal.Get(i => i.ImageId == imageId));

        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(IFormFile file, CarImage carImage)
        {

            var result = FileHelper.Upload(file);
            if (!result.SuccessStatus)
            {
                return new ErrorResult(result.Message);
            }

            carImage.ImagePath = result.Data;
            _imageDal.Add(carImage);
            return new SuccessResult("Car image added");
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(IFormFile file,CarImage carImage)
        {
            var image = _imageDal.Get(c => c.ImageId == carImage.ImageId);
            if (image == null)
            {
                return new ErrorResult("Image not found.");
            }

            var updatedFile = FileHelper.Update(file,image.ImagePath);
            if (!updatedFile.SuccessStatus)
            {
                return new ErrorResult(updatedFile.Message);
            }
            carImage.ImagePath = updatedFile.Data;
            _imageDal.Update(carImage);
            return new SuccessResult("Car image updated");
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            var image = _imageDal.Get(c => c.ImageId == carImage.ImageId);
            if (image == null)
            {
                return new ErrorResult("Image not found");
            }

            FileHelper.Delete(image.ImagePath);
            _imageDal.Delete(carImage);
            return new SuccessResult(Messages.Deleted);
        }

        private IResult CheckIfImageCount(CarImage carImage)
        {
            List<CarImage> gelAll = _imageDal.GetAll(i => i.CarId == carImage.CarId);
            var result = (gelAll.Count() >=5 );
            if (result)
            {
                return new ErrorResult("Bir aracın en fazla 5 resmi olabilir.");
            }
            return new SuccessResult();
        }
    }
}
