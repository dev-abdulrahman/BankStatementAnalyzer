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
    public class ClassifiedCategoryRepository : GenericRepository<BankStatementAnalyzerContext, ClassifiedCategory>, IClassifiedCategoryRepository
    {
        public ClassifiedCategoryRepository(BankStatementAnalyzerContext context) : base(context)
        {
        }
    }
}
