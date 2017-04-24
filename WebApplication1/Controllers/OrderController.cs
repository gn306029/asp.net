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
        /// 依條件取得訂單
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult GetOrderByCondition(Models.Class1 order)
        {
            Models.OrderService orderService = new Models.OrderService();
            //有空格的字放到value裡會讓value的值只取到前面 所以暫時先這樣做
            try
            {
                string[] shipperName = order.ShipperName.Split(' ');
                ViewBag.First_shipperName = shipperName[0];
                ViewBag.Second_shipperName = shipperName[1];
            }
            catch (Exception e)
            {
                ViewBag.First_shipperName = "S";
                ViewBag.Second_shipperName = " ";
            }
            ViewBag.Condition = order;
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
        public ActionResult UpdateOrder(Models.Class1 order,string[] ProductID,string[] UnitPrice , string[] Qty, string[] Discount)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.UpdateOrder(order.OrderID, order,ProductID,UnitPrice,Qty,Discount);
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
            OrderID = OrderID.Replace("/","");
            Models.OrderService orderService = new Models.OrderService();
            //取得 Order 資料
            ViewBag.Order = orderService.GetOrderById(OrderID);
            //取得 OrderDetail 資料
            ViewBag.OrderDetail = orderService.GetOrderDetail(OrderID);
            //所有的產品名稱
            ViewBag.ProductName = orderService.GetProductName();
            //所有的產品編號
            ViewBag.ProductID = orderService.GetProductID();
            //該筆訂單的產品名稱
            ViewBag.OrderProductName = orderService.GetOrderProductName(OrderID);
            string[] Date = (ViewBag.Order.OrderDate + "").Split(' ');
            ViewBag.OrderDate = convertDate(Date[0]);
            Date = (ViewBag.Order.RequireDate + "").Split(' ');
            ViewBag.RequireDate = convertDate(Date[0]);
            Date = (ViewBag.Order.ShippedDate + "").Split(' ');
            ViewBag.ShippedDate = convertDate(Date[0]);
            return View();
        }

        [HttpPost()]
        public JsonResult AjaxMethod(string OrderID, string CustomerName, string EmployeeName, string ShipperName, string OrderDate, string ShippedDate, string RequireDate)
        {
            DateTime datetime;
            Models.OrderService orderService = new Models.OrderService();
            Models.Class1 order = new Models.Class1();
            order.OrderID = OrderID;
            order.CustomerName = CustomerName;
            order.EmployeeName = EmployeeName;
            order.ShipperName = ShipperName;
            if (OrderDate != "") {
                order.OrderDate = Convert.ToDateTime(OrderDate);
            }else
            {
                order.OrderDate = null;
            }
            if (OrderDate != "")
            {
                order.ShippedDate = Convert.ToDateTime(ShippedDate);
            }
            else
            {
                order.ShippedDate = null;
            }
            if (OrderDate != "")
            {
                order.RequireDate = Convert.ToDateTime(RequireDate);
            }
            else
            {
                order.RequireDate = null;
            }
            List<Models.Class1> data = orderService.AjaxGetOrderByCondition(order,"ASC");
            var jsonData = Json(data, JsonRequestBehavior.AllowGet);
            return jsonData;
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