using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppSqlServerDataProvider.Data;
using WebAppSqlServerDataProvider.Models;

namespace WebAppDataProvider
{
    public class OrderDataProvider : IOrderDataProvider
    {
        #region [ Fields ]
        private readonly IDbContextFactory<FStoreDBContext> _dbContextFactory;
        #endregion

        #region [ CTor ]
        public OrderDataProvider(IDbContextFactory<FStoreDBContext> dbContextFactory) {
            _dbContextFactory = dbContextFactory;
        }
        #endregion

        #region [ CRUD ]
        public void AddOrder(Order order) {
            try {
                var tempOrder = this.GetOrderById(order.OrderId);
                if (tempOrder == null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Orders.Add(order);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
        public Order GetOrderById(int id) {
            Order tempOrder = null;
            try {
                using var context = _dbContextFactory.CreateDbContext();
                tempOrder = context.Orders.FirstOrDefault(x => x.OrderId == id);
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return tempOrder;
        }
        public void RemoveOrder(Order order) {
            try {
                Order tempOrder = this.GetOrderById(order.OrderId);
                if (tempOrder != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Orders.Remove(tempOrder);
                    context.SaveChanges();
                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        public void UpdateOrder(Order order) {
            try {
                Order tempOrder = this.GetOrderById(order.OrderId);
                if (tempOrder != null) {
                    using var context = _dbContextFactory.CreateDbContext();
                    context.Orders.Update(order);
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
        public List<Order> FilterOrderListByDateTime(DateTime from, DateTime to) {
            var OrderList = new List<Order>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                OrderList = context.Orders.Where(x => from <= x.OrderDate && x.OrderDate <= to).ToList().OrderByDescending(x => x.Freight).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return OrderList;
        }

        public List<Order> FilterOrderListByMemberId(int MemberId) {
            var OrderList = new List<Order>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                OrderList = context.Orders.Where(x => x.MemberId == MemberId).ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return OrderList;
        }

        public List<Order> GetOrderList() {
            var OrderList = new List<Order>();
            try {
                using var context = _dbContextFactory.CreateDbContext();
                OrderList = context.Orders.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return OrderList;
        }
        #endregion
    }
}
