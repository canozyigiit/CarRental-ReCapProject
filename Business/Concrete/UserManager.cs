﻿using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Security.Hashing;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
      // [SecuredOperation("admin")]
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<User>> GetAll()

        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }

        [ValidationAspect(typeof(UserValidator), Priority = 1)]
        [CacheRemoveAspect("IUserService.Get")]

        public IResult Delete(User user)
        {
            var result = _userDal.DeleteUserIfNotReturnDateNull(user);
            if (result)
            {
                return new SuccessResult(Messages.Deleted);
            }

            return new ErrorResult(Messages.NotDeleted);

        }

        [ValidationAspect(typeof(UserValidator), Priority = 1)]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        //[CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<List<OperationClaim>> GetClaimsByUserId(int userId)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaimsByUserId());
        }

        public IResult EditProfile(UserUpdateDto user)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            var userInfo = new User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userDal.Update(userInfo);
            return new SuccessResult(Messages.Updated);
        }

        [ValidationAspect(typeof(UserValidator), Priority = 1)]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }
        [CacheAspect]
        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }
    }

}
