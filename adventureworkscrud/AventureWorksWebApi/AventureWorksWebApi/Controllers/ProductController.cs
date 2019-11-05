using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AventureWorksWebApi.Models;
using AventureWorksWebApi.UnitOfWork;
using AventureWorksWebApi.Repository;

namespace AventureWorksWebApi.Controllers
{
    [RoutePrefix("Api/Product")]
    public class ProductController : ApiController
    {
        static AdventureWorks2017Entities objEntity = new AdventureWorks2017Entities();
        IUnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(objEntity);

        [HttpGet]
        [Route("GetAll")]
        public IQueryable<Product> GetAll()
        {
            try
            {
                return unitOfWork.ProductRepository.Entities.Take(10).AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetProductById/{Id}")]
        public IHttpActionResult GetProductById(string Id)
        {
            Product objEmp = new Product();
            int ID = Convert.ToInt32(Id);
            try
            {
                objEmp = unitOfWork.ProductRepository.Entities.Where(x => x.ProductID == ID).FirstOrDefault();
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertProduct")]
        public IHttpActionResult PostProduct(Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                unitOfWork.ProductRepository.Add(product);
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(product);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public IHttpActionResult PutProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product objProduct = new Product();
                objProduct = unitOfWork.ProductRepository.Entities.Where(x => x.ProductID == product.ProductID).FirstOrDefault();
                if (objProduct != null)
                {
                    objProduct.Name = product.Name;
                    objProduct.ProductNumber = product.ProductNumber;
                    objProduct.Color = product.Color;
                    objProduct.StandardCost = product.StandardCost;
                    objProduct.ListPrice = product.ListPrice;
                }
                unitOfWork.Commit();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(product);
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = unitOfWork.ProductRepository.Entities.Where(x => x.ProductID == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            unitOfWork.ProductRepository.Remove(product);
            objEntity.SaveChanges();

            return Ok(product);
        }
    }
}
