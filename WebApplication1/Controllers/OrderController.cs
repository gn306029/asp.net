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
        /// 新增訂單存檔的Action
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder()
        {
            //Models.OrderService orderService = new Models.OrderService();
            //orderService.InsertOrder(order);
            ViewBag.Desc = "ViewBag";
            ViewData["Desc2"] = "VIewData";
            TempData["Desc3"] = "TempData";

            return View();
            //return RedirectToAction("Index");
        }
        /// <summary>
        /// 新增訂單資訊
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="CustomerName"></param>
        /// <param name="OrderDate"></param>
        /// <param name="RequireDate"></param>
        /// <param name="ShippedDate"></param>
        /// <param name="ShipperID"></param>
        /// <param name="Freight"></param>
        /// <param name="ShipName"></param>
        /// <param name="ShipAddres"></param>
        /// <param name="ShipCity"></param>
        /// <param name="ShipRegion"></param>
        /// <param name="ShipPostalCode"></param>
        /// <param name="ShipCountry"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult CreatOrder(string OrderID,string CustomerID,string EmployeeID,string CustomerName,DateTime OrderDate,DateTime RequireDate,
                                       DateTime ShippedDate,string ShipperID,string Freight,string ShipName,string ShipAddres,string ShipCity,string ShipRegion,string ShipPostalCode,string ShipCountry)
        {
            Models.Class1 OrderDetail = new Models.Class1();
            OrderDetail.CustomerID = CustomerID;
            OrderDetail.EmployeeID = EmployeeID;
            OrderDetail.OrderDate = OrderDate;
            OrderDetail.RequireDate = RequireDate;
            OrderDetail.ShippedDate = ShippedDate;
            OrderDetail.ShipperID = ShipperID;
            OrderDetail.Freight = Convert.ToDecimal(Freight);
            OrderDetail.ShipName = ShipName;
            OrderDetail.ShipAddres = ShipAddres;
            OrderDetail.ShipCity = ShipCity;
            OrderDetail.ShipRegion = ShipRegion;
            OrderDetail.ShipPostalCode = ShipPostalCode;
            OrderDetail.ShipCountry = ShipCountry;

            Models.OrderService orderservice = new Models.OrderService();
            orderservice.InsertOrder(OrderDetail);

            ViewBag.one = OrderID;
            ViewBag.two = CustomerID;
            ViewBag.three = EmployeeID;
            ViewBag.four = CustomerName;
            ViewBag.five = OrderDate;
            ViewBag.six = RequireDate;
            ViewBag.seven = ShippedDate;
            ViewBag.eight = ShipperID;
            ViewBag.nine = Freight;
            ViewBag.ten = ShipName;
            ViewBag.eleven = ShipAddres+" \r\n";
            ViewBag.twelve = ShipRegion;
            ViewBag.threeten = ShipPostalCode;
            ViewBag.fourten = ShipCountry;

            return View();
        }
        


        /// <summary>
        /// 依ID取得訂單
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult GetOrderByID(string txtOrderId)
        {
            Models.OrderService orderService = new Models.OrderService();
            try
            {
                ViewBag.Save = orderService.GetOrderById(txtOrderId);
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

        [HttpPost()]
        public ActionResult DeleteOrder(string OrderID)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.DeleteOrderByID(OrderID);
            return View();
        }
    }
}