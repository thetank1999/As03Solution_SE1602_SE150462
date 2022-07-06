using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebAppDataProvider;
using WebAppSqlServerDataProvider.Models;

namespace WebApp.Controllers
{
    public class MemberController : Controller
    {
        #region [ Fields ]
        private readonly IMemberDataProvider _memberDataProvider;
        private readonly IConfiguration _configuration;
        #endregion

        #region [ CTor ]
        public MemberController(IMemberDataProvider memberDataProvider, 
                                IConfiguration configuration) {
            this._memberDataProvider = memberDataProvider;
            this._configuration = configuration;
        }
        #endregion

        public IActionResult Index() {
            var memberList = this._memberDataProvider.GetMemberList();
            return View(memberList);
        }

        public IActionResult Details(int id) {
            var member = this._memberDataProvider.GetMemberById(id);
            return View(member);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var member = this._memberDataProvider.GetMemberById(id);
            return View(member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Member member) {
            try {
                var tempMemberEmail = _memberDataProvider.GetMemberByEmail(member.Email);
                if (ModelState.IsValid && tempMemberEmail.MemberId == member.MemberId || tempMemberEmail ==null) {
                    _memberDataProvider.UpdateMember(member);
                    return RedirectToAction(nameof(Index));
                } else {
                    ViewBag.MessageEmail = "Email cannot be dupplicated";
                    return View();
                }
                
            } catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();                
            }
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var member = this._memberDataProvider.GetMemberById(id);
            return View(member);
        }


        [HttpPost]
        public IActionResult Delete(Member member) {
            _memberDataProvider.RemoveMember(member);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Member member) {
            try {
                var tempMemberEmail = _memberDataProvider.GetMemberByEmail(member.Email);
                if (ModelState.IsValid && tempMemberEmail == null) {
                    _memberDataProvider.AddMember(member);
                    return RedirectToAction(nameof(Index));
                } else if(tempMemberEmail != null) {
                    ViewBag.MessageEmail = "Email cannot be dupplicated";
                    return View();
                } else {
                    ViewBag.MessageId = "Id cannot be dupplicated";
                    return View();
                }

            } catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
