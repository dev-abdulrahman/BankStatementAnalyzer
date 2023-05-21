namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IAppConfigurationService
    {
        string PayUkey { get; }

        string PayUSalt { get; }

        string PayULink { get; }

        string AdminBaseUrl { get; }

        string Partner { get; }

        string FromMail { get; }

        string AdminEmail { get; }

        string AdminName { get; }   
    }
}