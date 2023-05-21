using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankStatementAnalyzer.WebUI.ViewModels
{
    public class BaseModelViewModel
    {
        public BaseModelViewModel()
        {
            Status = true;
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public bool Status { get; set; }

        [StringLength(100)]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [StringLength(1000)]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }

        [StringLength(100)]
        [ScaffoldColumn(false)]
        [JsonIgnore]
        public string DeletedBy { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime? UpdatedDate { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [JsonIgnore]
        public DateTime? DeletedDate { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        [JsonIgnore]
        public bool CreateAnother { get; set; }
    }
}