using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Web;
using System.Collections.Generic;
using WebAppDataProvider;
using WebAppSqlServerDataProvider.Models;
using Microsoft.AspNetCore.Http;
using WebAppViewModelProvider;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        #region [ Fields ]
        private readonly IOrderDataProvider _orderDataProvider;
        private readonly IOrderDetailDataProvider _orderDetailDataProvider;
        private readonly IMemberDataProvider _memberDataProvider;
        private readonly IProductDataProvider _productDataProvider;
        #endregion

        #region [ Ctor ]
        public OrderController( IOrderDataProvider orderDataProvider,
                                IOrderDetailDataProvider orderDetailDataProvider,
                                IMemberDataProvider memberDataProvider,
                                IProductDataProvider productDataProvider) {
            this._orderDataProvider = orderDataProvider;
            this._orderDetailDataProvider = orderDetailDataProvider;
            this._memberDataProvider = memberDataProvider;
            this._productDataProvider = productDataProvider;
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
            var oderViewModel = new OrderViewModel();
            oderViewModel._order = _orderDataProvider.GetOrderById(id);
            oderViewModel._orderDetailList = _orderDetailDataProvider.FilterOrderDetailListByOrderId(oderViewModel._order.OrderId);
            oderViewModel._order.Member = _memberDataProvider.GetMemberById(oderViewModel._order.MemberId.Value);

            foreach (var orderDetail in oderViewModel._orderDetailList) {
                orderDetail.Product = _productDataProvider.GetProductById(orderDetail.ProductId);
            }

            return View(oderViewModel);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderViewModel orderViewModel) {
            try {
                var tempOrderDb = _orderDataProvider.GetOrderById(orderViewModel._order.OrderId);
                if (tempOrderDb != null) {
                    ViewBag.MessageId = "Order Id cannot be dupplicated";
                    return View();
                } else if (orderViewModel._orderDetailList.Count < 1) {
                    ViewBag.MessageOrderDetail = "Order detail cannot be empty.";
                    return View();
                } else {
                    _orderDataProvider.AddOrder(orderViewModel._order);
                    foreach (var orderDetail in orderViewModel._orderDetailList) {
                        _orderDetailDataProvider.AddOrderDetail(orderDetail);
                    }
                    if (HttpContext.Session.GetInt32("UserId") > -1) {
                        return RedirectToAction(nameof(OrderByMember));
                    } else {
                        return RedirectToAction(nameof(Index));
                    }
                }
            } catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
