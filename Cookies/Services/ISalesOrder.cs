using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface ISalesOrder
    {
       List<SalesOrder> GetSalesOrders();
       SalesOrder GetSalesOrder(int id);
       DbResult removeSalesOrder(int id);
       SalesOrderDetail AddSalesOrderDetail(int sod_product, int so_customer, int sod_qty);
       DbResult createSalesOrder(int so_customer, string sod_data, int u_id);

       List<List_SalesOrderDetail> getSalesOrderDetail(int id);


       DbResult convertSalesOrderToInvoice(int so_id);
    }
}
