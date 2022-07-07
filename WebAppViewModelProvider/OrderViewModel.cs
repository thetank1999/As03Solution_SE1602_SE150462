using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppSqlServerDataProvider.Models;

namespace WebAppViewModelProvider
{
    public class OrderViewModel
    {
        public Order _order { get; set; }
        public List<OrderDetail> _orderDetailList { get; set; }
    }
}
