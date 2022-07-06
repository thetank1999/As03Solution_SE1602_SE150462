using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Web;
using System.Collections.Generic;
using WebAppDataProvider;
using WebAppSqlServerDataProvider.Models;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        #region [ Fields ]
        private readonly IOrderDataProvider _orderDataProvider;
        private readonly IOrderDetailDataProvider _orderDetailDataProvider;
        private readonly IMemberDataProvider _memberDataProvider;

        #endregion

        #region [ Ctor ]
        public OrderController( IOrderDataProvider orderDataProvider,
                                IOrderDetailDataProvider orderDetailDataProvider,
                                IMemberDataProvider memberDataProvider) {
            this._orderDataProvider = orderDataProvider;
            this._orderDetailDataProvider = orderDetailDataProvider;
            this._memberDataProvider = memberDataProvider;
        }
        #endregion
        public IActionResult Index() {
            var orderList = this._orderDataProvider.GetOrderList();
            foreach (var order in orderList) {
                order.Member = _memberDataProvider.GetMemberById(order.MemberId.Value);
            }
            return View(orderList);
        }

        public IActionResult OrderByMember() {

            var id = HttpContext.Session.GetInt32("UserId");
            
            var orderList = _orderDataProvider.FilterOrderListByMemberId(id.Value);
            foreach (var order in orderList) {
                order.Member = _memberDataProvider.GetMemberById(order.MemberId.Value);
            }
            return View(orderList);
        }
        [HttpGet]
        public IActionResult Details(int id) {
            var order = _orderDataProvider.GetOrderById(id);
            var orderDetailList = _orderDetailDataProvider.FilterOrderDetailListByOrderId(order.OrderId);
            order.Member = _memberDataProvider.GetMemberById(order.MemberId.Value);
            return View(order);
        }
    }
}
