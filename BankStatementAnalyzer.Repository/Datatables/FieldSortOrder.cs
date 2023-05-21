using System;
using System.Linq;

namespace BankStatementAnalyzer.Repository.DataTables
{
    public class FieldSortOrder<T> : ISortCriteria<T> where T : class
    {
        //-----------------------------------------------------------------------
        public String Name { get; set; }

        //-----------------------------------------------------------------------
        public SortDirection Direction { get; set; }

        //-----------------------------------------------------------------------
        public FieldSortOrder()
        {
            this.Direction = SortDirection.Ascending;
        }

        //-----------------------------------------------------------------------
        public FieldSortOrder(String name, SortDirection direction)
            : base()
        {
            Name = name;
            Direction = direction;
        }

        //-----------------------------------------------------------------------
        public IOrderedQueryable<T> ApplyOrdering(IQueryable<T> qry, Boolean useThenBy)
        {
            IOrderedQueryable<T> result = null;
            var descending = this.Direction == SortDirection.Descending;
            result = !useThenBy ? qry.OrderBy(Name, descending) : qry.ThenBy(Name, descending);
            return result;
        }
    }

}
