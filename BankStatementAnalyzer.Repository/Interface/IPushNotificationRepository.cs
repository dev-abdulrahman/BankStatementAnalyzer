using BankStatementAnalyzer.Models;
using BankStatementAnalyzer.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.Repository.Interface
{
    public interface IPushNotificationRepository : IGenericRepository<PushNotification>
    {
    }
}
