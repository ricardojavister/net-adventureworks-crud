using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AventureWorksWebApi.Models;
using AventureWorksWebApi.Repository;

namespace AventureWorksWebApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdventureWorks2017Entities _dbContext;
        #region Repositories
        public IRepository<Person> PersonRepository =>
           new Repository<Person>(_dbContext);
        public IRepository<Product> ProductRepository =>
           new Repository<Product>(_dbContext);
        public IRepository<ProductSubcategory> ProductSubcategoryRepository =>
          new Repository<ProductSubcategory>(_dbContext);
        public IRepository<ProductCategory> ProductCategoryRepository =>
          new Repository<ProductCategory>(_dbContext);
        public IRepository<UnitMeasure> UnitMeasureRepository =>
          new Repository<UnitMeasure>(_dbContext);
        public IRepository<ProductModel> ProductModelRepository =>
          new Repository<ProductModel>(_dbContext);
        #endregion
        public UnitOfWork(AdventureWorks2017Entities dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}