﻿using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
   public class UserManager:IUserService
   {
       private IUserDal _userDal;

       public UserManager(IUserDal userDal)
       {
           _userDal = userDal;
       }

       public IDataResult<List<User>> GetAll()
       {
           return new SuccessDataResult<List<User>>(_userDal.GetAll());
       }

       public IDataResult<User> GetById(int userId)
       {
           return new SuccessDataResult<User>(_userDal.Get(u=>u.Id==userId));
       }

       [ValidationAspect(typeof(UserValidator))]
       public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

       public IResult Delete(User user)
       {
           _userDal.Delete(user);
           return new SuccessResult(Messages.UserDeleted);
       }

       public IResult Update(User user)
       {
           _userDal.Update(user);
           return new SuccessResult(Messages.UserUpdated);
       }
   }
}
