using AventureWorksWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventureWorksWebApi.Models;

namespace AventureWorksWebApi.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Person> PersonRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductSubcategory> ProductSubcategoryRepository { get; }
        IRepository<ProductCategory> ProductCategoryRepository { get; }
        IRepository<UnitMeasure> UnitMeasureRepository { get; }
        IRepository<ProductModel> ProductModelRepository { get; }
        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();
        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();
        void Dispose();
    }
}
