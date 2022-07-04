using System.Collections.Generic;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public interface IOrderDetailDataProvider
    {
        public List<OrderDetail> FilterOrderDetailListByOrderId(int orderId);
        public List<OrderDetail> FilterOrderDetailListByProductId(int productId);
        public List<OrderDetail> GetOrderDetailList();
        public OrderDetail GetOrderDetailByProductId_OrderId(int productId, int orderId);
        public void AddOrderDetail(OrderDetail orderId);
        public void UpdateOrderDetail(OrderDetail orderId);
        public void RemoveOrderDetail(OrderDetail orderId);
    }
}
