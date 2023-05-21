namespace BankStatementAnalyzer.Models
{
    public class ClassifiedArticle : BaseModel
    {
        public string Name { get; set; }

        public string SubHeading { get; set; }

        public string Heading { get; set; }

        public string ShortDescription { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string Article { get; set; }
    }
}
