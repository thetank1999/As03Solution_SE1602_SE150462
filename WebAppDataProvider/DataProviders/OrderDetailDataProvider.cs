using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppSqlServerDataProvider.Data;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public class OrderDetailDataProvider : IOrderDetailDataProvider
    {
        #region [ Fields ]
        private readonly IDbContextFactory<FStoreDBContext> _dbContextFactory;
        #endregion

        #region [ CTor ]
        public OrderDetailDataProvider(IDbContextFactory<FStoreDBContext> dbContextFactory) {
            _dbContextFactory = dbContextFactory;
        }
        #endregion

        #region [ CRUD ]
        public void AddOrderDetail(OrderDetail orderId) {
            try {
                var _tempOrderDetail = this.GetOrderDetailByProductId_OrderId(orderId.ProductId, orderId.OrderId);
                if (_tempOrderDetail == null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.OrderDetails.Add(orderId);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        public OrderDetail GetOrderDetailByProductId_OrderId(int productId, int orderId) {
            OrderDetail tempOrderDetail = null;
            try {
                using var context = _dbContextFactory.CreateDbContext();
                tempOrderDetail = context.OrderDetails.FirstOrDefault(x => x.ProductId == productId && x.OrderId == orderId);
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return tempOrderDetail;
        }
        public void RemoveOrderDetail(OrderDetail orderId) {
            try {
                OrderDetail _tempOrderDetail = this.GetOrderDetailByProductId_OrderId(orderId.ProductId, orderId.OrderId);
                if (_tempOrderDetail != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.OrderDetails.Remove(_tempOrderDetail);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        public void UpdateOrderDetail(OrderDetail orderId) {
            try {
                OrderDetail tempOrderDetail = this.GetOrderDetailByProductId_OrderId(orderId.ProductId, orderId.OrderId);
                if (tempOrderDetail != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.OrderDetails.Update(orderId);
                    context.SaveChanges();

                } else {
                    throw new Exception();

                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region [Method - Lists]
        public List<OrderDetail> FilterOrderDetailListByOrderId(int orderId) {
            var orderDetailList = new List<OrderDetail>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                orderDetailList = context.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return orderDetailList;
        }

        public List<OrderDetail> FilterOrderDetailListByProductId(int productId) {
            var orderDetailList = new List<OrderDetail>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                orderDetailList = context.OrderDetails.Where(x => x.ProductId == productId).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return orderDetailList;
        }
        public List<OrderDetail> GetOrderDetailList() {
            var orderDetailList = new List<OrderDetail>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                orderDetailList = context.OrderDetails.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return orderDetailList;
        }
        #endregion

    }
}
