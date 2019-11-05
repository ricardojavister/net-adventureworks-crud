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
    [RoutePrefix("Api/Person")]
    public class PersonController : ApiController
    {
        static AdventureWorks2017Entities objEntity = new AdventureWorks2017Entities();
        IUnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(objEntity);

        [HttpGet]
        [Route("GetAll")]
        public IQueryable<Person> GetAll()
        {
            try
            {
                return unitOfWork.PersonRepository.Entities.Where(x => x.PersonType == "EM").AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetPersonById/{Id}")]
        public IHttpActionResult GetPersonById(string Id)
        {
            Person objEmp = new Person();
            int ID = Convert.ToInt32(Id);
            try
            {
                objEmp = unitOfWork.PersonRepository.Entities.Where(x => x.BusinessEntityID == ID).FirstOrDefault();
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
 
    }
}
