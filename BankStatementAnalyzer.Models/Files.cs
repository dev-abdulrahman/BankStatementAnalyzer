using System;
using System.ComponentModel.DataAnnotations;

namespace BankStatementAnalyzer.Models
{
    public class File
    {
        public File()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Path { get; set; }

        [Required]
        public string EntityName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Extension { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(1000)]
        public string UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }
    }
}