using BankStatementAnalyzer.BusinessLogic.ViewModels;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IDashboardService
    {
        Dashboard GetWhatsAppCount();

        Dashboard GetOneTouchCount();

        Dashboard GetCustomizeCount();

        Dashboard GetPackageCount();
    }
}
