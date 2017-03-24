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
            Models.OrderService orderService = new Models.OrderService();
            orderService.GetOrderById("10250");
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
        /// <summary>
        /// 依ID取得訂單
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult MyAction(string txtOrderId)
        {
            Models.OrderService orderService = new Models.OrderService();
            Models.Class1 OrderDetail = new Models.Class1();
            OrderDetail = orderService.GetOrderById(txtOrderId);
            var saveArray = new string[17];
            try
            {
                saveArray[0] = "CustomerID：" + OrderDetail.CustomerID;
                saveArray[1] = "CustomerName：" + OrderDetail.CustomerName;
                saveArray[2] = "EmployeeID：" + OrderDetail.EmployeeID;
                saveArray[3] = "EmployeeName：" + OrderDetail.EmployeeName;
                saveArray[4] = "Freight：" + OrderDetail.Freight;
                saveArray[5] = "OrderDate：" + OrderDetail.OrderDate;
                saveArray[6] = "OrderID：" + OrderDetail.OrderID;
                saveArray[7] = "RequireDate：" + OrderDetail.RequireDate;
                saveArray[8] = "ShipAddres：" + OrderDetail.ShipAddres;
                saveArray[9] = "ShipCity：" + OrderDetail.ShipCity;
                saveArray[10] = "ShipCountry：" + OrderDetail.ShipCountry;
                saveArray[11] = "ShipName：" + OrderDetail.ShipName;
                saveArray[12] = "ShipperDate：" + OrderDetail.ShippedDate;
                saveArray[13] = "ShipperID：" + OrderDetail.ShipperID;
                saveArray[14] = "ShipperName：" + OrderDetail.ShipperName;
                saveArray[15] = "ShipPostalCode：" + OrderDetail.ShipPostalCode;
                saveArray[16] = "ShipRegion：" + OrderDetail.ShipRegion;
                ViewBag.Collection = saveArray;
                return View();
            }catch(Exception e)
            {
                ViewBag.Error = "沒有這個ID呦";
                return View();
            }
        }

        /// <summary>
        /// 依要求日期由近到遠取得前10筆訂單
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult OrderByDate()
        {
            Models.OrderService orderService = new Models.OrderService();
            List<Models.Class1> OrderDetail = new List<Models.Class1>();
            OrderDetail = orderService.GetOrderByRequireDate();
            ViewBag.List = OrderDetail;
            return View();
        }
    }
}