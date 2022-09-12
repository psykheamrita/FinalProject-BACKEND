using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // -----> ATTRIBUTE
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //naming convention
        //Ioc Container -- Inversion of Control
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);

            ///Dependency chain ---
            
            //IProductService productService = new ProductManager(new EfProductDal());
            //return new List<Product>
            //{
            //    new Product
            //    {
            //        ProductName = "Elma", 
            //        ProductId = 1,
            //        CategoryId = 1,
            //        UnitPrice = 15,
            //        QuantityPerUnit = "1500",
            //        UnitsInStock = 30
            //    },
            //    new Product
            //    {
            //        ProductName = "Armut",
            //        ProductId = 2,
            //        CategoryId = 1,
            //        UnitPrice = 25,
            //        QuantityPerUnit = "2000",
            //        UnitsInStock = 20
            //    }
            //};
        }
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetByCategoryId(categoryId); 
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }

}
