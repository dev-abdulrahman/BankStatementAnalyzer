using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.DataTables;
using System.Globalization;
using System.Linq;

namespace BankStatementAnalyzer.SuperAdmin.ViewModels.Datatables
{
    public class DataTablesFormat
    {
        public static object Orders(DataTablesPageRequest pageRequest, Page<object> report) => new
        {
            //sEcho = pageRequest.Echo,
            //iTotalRecords = report.TotalItems,
            //iTotalDisplayRecords = report.TotalItems,
            //sColumns = pageRequest.ColumnNames,
            //aaData = (from i in report.Items
            //          select new[]
            //               {
            //                i.ID.ToString(CultureInfo.InvariantCulture),
            //                i.Customer.Name,
            //                i.Total.ToString(),
            //                i.TransactionID ??"-",
            //                i.PaymentType.ToString(),
            //                i.SystemOrderID
            //                   }).ToList()
        };
    }
}