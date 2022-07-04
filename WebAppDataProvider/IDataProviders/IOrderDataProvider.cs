using System;
using System.Collections.Generic;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider 
{ 
    public interface IOrderDataProvider
    {
        public List<Order> FilterOrderListByMemberId(int MemberId);
        public List<Order> GetOrderList();
        public Order GetOrderById(int id);
        public void AddOrder(Order order);
        public void UpdateOrder(Order order);
        public List<Order> FilterOrderListByDateTime(DateTime from, DateTime to);
        public void RemoveOrder(Order order);
    }
}
