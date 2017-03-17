using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        /// <summary>
        /// 訂單管理系統首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            /*Models.OrderService orderService = new Models.OrderService();
            var order = orderService.GetOrderById("123");
            ViewBag.CustomerID = order.CustomerID;
            ViewBag.CustomerName = order.CustomerName;*/
            ViewBag.test = "test";
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.Data = orderService.GetOrders();
            return View();
        }
        /// <summary>
        /// 新增訂單畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertOrder()
        {
            return View();
        }
        /// <summary>
        /// 新增訂單存檔的Action
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder(Models.Class1 order)
        {
            //Models.OrderService orderService = new Models.OrderService();
            //orderService.InsertOrder(order);
            ViewBag.Desc = "ViewBag";
            ViewData["Desc2"] = "VIewData";
            TempData["Desc3"] = "TempData";
            //return View("Index2");
            return RedirectToAction("Index");
        }
        [HttpGet()]
        public JsonResult TestJson()
        {
            var result = new Models.Class1(); // 等於 var result = new Models.Order() { CustomerID = "GSS" , CustomerName = "瑞陽資訊" };
            result.CustomerID = "GSS";
            result.CustomerName = "瑞陽資訊";
            return this.Json(result, JsonRequestBehavior.AllowGet); //方便Demo 故用 Get
        }


    }
}