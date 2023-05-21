using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using BankStatementAnalyzer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Repository.Impl
{
    public class CarouselRepository : GenericRepository<BankStatementAnalyzerContext, Carousel>, ICarouselRepository
    {
        public CarouselRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
