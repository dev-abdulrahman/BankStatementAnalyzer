using System;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IMemoryCacheService
    {
        T GetOrSet<T>(string memoryCacheKey, Func<T> getItemCallback) where T : class;
        void Clear(string memoryCacheKey);
    }
}