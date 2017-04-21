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
            ViewBag.ShipperName = orderService.GetOrderShipper();
            ViewBag.EmployeeName = orderService.GetOrderEmployee();
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
            Models.OrderService orderService = new Models.OrderService();
            //ViewBag.ProductName = orderService.GetProductName();
            return View();
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
        public ActionResult CreatOrder(string OrderID, string CustomerID, string EmployeeID, string CustomerName, DateTime OrderDate, DateTime RequireDate,
                                       DateTime ShippedDate, string ShipperID, string Freight, string ShipName, string ShipAddres, string ShipCity, string ShipRegion, string ShipPostalCode, string ShipCountry)
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
            ViewBag.eleven = ShipAddres + " \r\n";
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
        public ActionResult GetOrderByCondition(Models.Class1 order)
        {
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.Save = orderService.GetOrderByCondition(order);
            ViewBag.model = order;
            return View();
        }

        /// <summary>
        /// 依 OrderID 刪除訂單
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult DeleteOrder(string OrderID)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.DeleteOrderByID(OrderID);
            return View();
        }


        [HttpPost()]
        public ActionResult UpdateOrder(Models.Class1 order)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.UpdateOrder(order.OrderID, order);
            return View();
        }

        /// <summary>
        /// 取得訂單資料，要塞進框框裡，看使用者要更新哪個
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult GetOrderDetail(string OrderID)
        {
            Models.OrderService orderService = new Models.OrderService();
            //取得 Order 資料
            ViewBag.OrderDetail = orderService.GetOrderById(OrderID);
            //取得 OrderDetail 資料
            ViewBag.OrderDetail2 = orderService.GetOrderDetail(OrderID);
            ViewBag.ProductName = orderService.GetProductName();
            string[] Date = (ViewBag.OrderDetail.OrderDate + "").Split(' ');
            ViewBag.OrderDate = convertDate(Date[0]);
            Date = (ViewBag.OrderDetail.RequireDate + "").Split(' ');
            ViewBag.RequireDate = convertDate(Date[0]);
            Date = (ViewBag.OrderDetail.ShippedDate + "").Split(' ');
            ViewBag.ShippedDate = convertDate(Date[0]);
            return View();
        }

        /// <summary>
        /// 轉換成我要的日期格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string convertDate(string date)
        {
            string[] save = date.Split('/');
            if (save[1].Length == 1)
            {
                save[1] = "0" + save[1];
            }
            if (save[2].Length == 1)
            {
                save[2] = "0" + save[2];
            }
            return save[0] + "-" + save[1] + "-" + save[2];
        }
    }
}