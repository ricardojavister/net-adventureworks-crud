using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AventureWorksWebApi.UnitOfWork;
using AventureWorksWebApi.Controllers;
using AventureWorksWebApi.Models;
using System.Linq;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        static AdventureWorks2017Entities dbContext = new AdventureWorks2017Entities();
        IUnitOfWork unitOfWork = new UnitOfWork(dbContext);
   
        [TestMethod]
        public void TestGetAll() {
            ProductController productController = new ProductController();
            var resultGetAll = productController.GetAll();
        }

        [TestMethod]
        public void TestGetById()
        {
            ProductController productController = new ProductController();
            var resultGetAll = productController.GetProductById("711");
        }

        [TestMethod]
        public void TestAdd()
        {
            ProductController productController = new ProductController();
            Product newProduct = new Product { Name = "New Product", ProductNumber = "124", MakeFlag = true, FinishedGoodsFlag = true, Color = "White", SafetyStockLevel = 2, ReorderPoint = 2, StandardCost = 15000, ListPrice = 25600, Size = "2", SizeUnitMeasureCode = "BOX", WeightUnitMeasureCode = "BOX", Weight = 234, DaysToManufacture = 52, ProductLine = "R", Class = "H", Style = "U", ProductSubcategoryID = 6, ProductModelID = 6, SellStartDate = DateTime.Today, SellEndDate = DateTime.Today, DiscontinuedDate = DateTime.Today, ModifiedDate = DateTime.Today };
            var resultGetAll = productController.PostProduct(newProduct);
        }

        [TestMethod]
        public void TestGetUpdate()
        {
            ProductController productController = new ProductController();
            var productbyidnumber = unitOfWork.ProductRepository.Entities.Where(x => x.ProductID == 1005).FirstOrDefault();
            var resultGetAll = productController.PutProduct(productbyidnumber);
        }

        [TestMethod]
        public void TestRemove()
        {
            ProductController productController = new ProductController();
            var productbyidnumber = unitOfWork.ProductRepository.Entities.Where(x => x.ProductID == 1003).FirstOrDefault();
            var resultGetAll = productController.DeleteProduct(productbyidnumber.ProductID);
        }

    }
}