using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.DataTables;
using System.Globalization;
using System.Linq;

namespace BankStatementAnalyzer.WebUI.ViewModels.Datatables
{
    public class DataTablesFormat
    {
        public static object Orders(DataTablesPageRequest pageRequest, Page<Order> report) => new
        {
            sEcho = pageRequest.Echo,
            iTotalRecords = report.TotalItems,
            iTotalDisplayRecords = report.TotalItems,
            sColumns = pageRequest.ColumnNames,
            aaData = (from i in report.Items
                      select new[]
                           {
                            i.ID.ToString(CultureInfo.InvariantCulture),
                            i.OrderStatus.ToString(),
                            i.OrderType.ToString(),
                            i.Customer?.Username,
                            i.UpdatedDate.ToString()
                               }).ToList()
        };
    }
}