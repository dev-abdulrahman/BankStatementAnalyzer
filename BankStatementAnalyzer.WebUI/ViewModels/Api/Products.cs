namespace BankStatementAnalyzer.WebUI.ViewModels.Api
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public string Size { get; set; }

        public bool IsProductReturnable { get; set; }
    }
}