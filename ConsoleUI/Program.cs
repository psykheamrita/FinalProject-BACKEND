using Business.Concrete;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemeory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    //SOLID
    //Open Close Principle
    public class Program
    {
        static void Main(string[] args)
        {
            //Data Transformation Object

            //Ioc

            //ProductTest();

            //CategoryTest();

            //ProductDetailsTest();
        }

        private static void ProductDetailsTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
            var result = productManager.GetProductDetails();
            if (result.Success == true)
            {
                Console.WriteLine("ProductId" + " / " + "ProductName" + " / " + "CategoryName" + " / " + "UnitsInStock");
                foreach (var pDto in result.Data)
                {
                    Console.WriteLine(pDto.ProductId + " / " + pDto.ProductName + " / " + pDto.CategoryName + " / " + pDto.UnitsInStock);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void CategoryTest()
        {

            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var c in categoryManager.GetAll().Data)
            {
                Console.WriteLine(c.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
            var result = productManager.GetAll();
            if (result.Success == true)
            {
                foreach (var p in productManager.GetAll().Data)
                {
                    Console.WriteLine(p.ProductName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Console.WriteLine("\n");
            foreach (var p in productManager.GetByCategoryId(2).Data)
            {
                Console.WriteLine(p.ProductName);
            }
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            foreach (var p in productManager.GetByUnitPrice(18, 43).Data)
            {
                Console.WriteLine(p.ProductName);
            }
            Console.WriteLine("\n");
            Console.WriteLine(productManager.GetById(5).Data.ProductName);
        }
    }
}