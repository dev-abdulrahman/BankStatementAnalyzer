using AutoMapper;

namespace BankStatementAnalyzer.WebUI.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
            });
        }
    }
}