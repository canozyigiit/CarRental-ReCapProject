﻿using Core.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator :AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.BrandName).NotEmpty().WithMessage(AspectMessages.CanNotBeBlank);
            RuleFor(b => b.BrandName).Length(2,20);
            
           // RuleFor(b => b.BrandModel).NotEmpty().WithMessage(AspectMessages.CanNotBeBlank);
           // RuleFor(b => b.BrandModel).Length(2, 20);
        }
    }
}