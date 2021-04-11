using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int userId);
        IResult Delete(User user);
        IResult Update(User user);
        IResult EditProfile(UserUpdateDto user);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<OperationClaim>> GetClaimsByUserId(int userId);

        IResult Add(User user);
        IDataResult<User> GetByEmail(string email);
    }
}
