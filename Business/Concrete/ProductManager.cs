using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.DependencyResolvers.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidationTools;
using Core.Entities.Concrete;
using Core.Utilities.ForBusiness;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //Claim
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if(CheckIfAddedProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }
            //}

            //IResult iResult = BusinessRules.Run(CheckIfAddedProductNameExists(product.ProductName), 
            //    CheckIfProductCountOfCategoryCorrect(product.ProductId), CheckIfLimitForNumberOfElementsInCategoryExceeded());
            //if (iResult != null)
            //{
            //    return iResult;
            //}
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductListed);
        }

        private IResult CheckIfLimitForNumberOfElementsInCategoryExceeded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new UnSuccessResult();
            }
            return new SuccessResult();

        }
        private IResult CheckIfAddedProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new UnSuccessResult();
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            if (_productDal.GetAll(p => p.CategoryId == categoryId).Count() > 15)
            {
                return new UnSuccessResult();
            }
            return new SuccessResult();
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.ProductsListed);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductDetailListed);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<13233)
            {
                throw new Exception("ananı sikim");
                return new UnSuccessResult(Messages.ProductsNotListed);
            }
            Add(product);
            return new SuccessResult(Messages.ProductListed);
        }
    }
}