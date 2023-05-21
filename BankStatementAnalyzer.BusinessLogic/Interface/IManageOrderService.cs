using BankStatementAnalyzer.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IManageOrderService
    {
        OrderVM GetOrderDetailsByOrderId(int id);

        Tuple<List<OrderResponse>, List<OrderResponse>> GetTop20Orders(int customerId);

        OrderResponse GetOrderById(int orderId);
    }
}