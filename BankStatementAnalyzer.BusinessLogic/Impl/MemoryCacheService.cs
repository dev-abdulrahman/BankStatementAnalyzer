using BankStatementAnalyzer.BusinessLogic.Interface;
using System;
using System.Runtime.Caching;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class MemoryCacheService : IMemoryCacheService
    {
        public T GetOrSet<T>(string memoryCacheKey, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(memoryCacheKey) as T;

            if (item == null)
            {
                item = getItemCallback();
                if (item != null) MemoryCache.Default.Add(memoryCacheKey, item, DateTime.Now.AddMinutes(60));
            }

            return item;
        }

        public void Clear(string memoryCacheKey)
        {
            if (MemoryCache.Default.Get(memoryCacheKey) != null)
            {
                MemoryCache.Default.Remove(memoryCacheKey);
            }
        }
    }
}