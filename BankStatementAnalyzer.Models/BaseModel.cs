using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankStatementAnalyzer.Models
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            Status = true;
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

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