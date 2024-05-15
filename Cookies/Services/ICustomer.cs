using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface ICustomer
    {
       List<Customer> getCustomers();
       DbResult createOrEditCustomer(Customer customer);
       Customer getCustomer(int id);
       DbResult removeCustomer(int id);
       List<CustomerLedger> getCustomerTransactions(int c_id);
    }
}
