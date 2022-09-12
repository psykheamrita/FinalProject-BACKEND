using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.DependencyResolvers.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).Length(2,200);
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitsInStock).GreaterThanOrEqualTo<Product,short>(15);
            RuleFor(p => p.QuantityPerUnit).Length(5, 150);

            //RuleFor(p=>p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //RuleFor(p => p.ProductName).Must(StartwithA).WithMessage(Messages.ProductNameErrorMessage);
        }
        //public bool StartwithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}
    }
}
