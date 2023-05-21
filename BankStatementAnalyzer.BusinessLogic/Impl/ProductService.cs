using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class ProductService : IProductService
    {
        private readonly Lazy<IProductRepository> productRepository;

        public ProductService(Lazy<IProductRepository> productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Add(Product entity)
        {
            productRepository.Value.Add(entity);
        }

        public void Delete(Product entity)
        {
            productRepository.Value.Delete(entity);
        }

        public void Edit(Product entity)
        {
            productRepository.Value.Edit(entity);
        }

        public IQueryable<Product> FindBy(Expression<Func<Product, bool>> predicate)
        {
            return productRepository.Value.FindBy(predicate);
        }

        public IQueryable<Product> GetAll()
        {
            return productRepository.Value.GetAll();
        }

        public void Save()
        {
            productRepository.Value.Save();
        }
    }
}
