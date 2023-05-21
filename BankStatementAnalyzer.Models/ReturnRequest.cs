using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public enum ReturnRequestType { Empty, Filled }

    public enum Status { Created, Cancelled, Assigned, Completed }

    public class ReturnRequest : BaseModel
    {
        [Required]
        public int NoOfCans { get; set; }

        public ReturnRequestType ReturnRequestType { get; set; }

        [Required]
        public string Remark { get; set; }

        public int AddressId { get; set; }

        public string CustomerUID { get; set; }

        public Status OrderStatus { get; set; }

        [NotMapped]
        public string CustomerName { get; set; }
    }
}