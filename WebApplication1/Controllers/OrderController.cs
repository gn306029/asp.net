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
        public ActionResult CreatOrder(Models.Order order, string[] ProductID, string[] UnitPrice, string[] Qty, string[] Discount)
        {
            Models.OrderService orderservice = new Models.OrderService();
            orderservice.InsertOrder(order,ProductID,UnitPrice,Qty,Discount);
            return View();
        }

        /// <summary>
        /// 依條件取得訂單
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult GetOrderByCondition(Models.Order order)
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
            OrderID = OrderID.Replace("/","");
            Models.OrderService orderService = new Models.OrderService();
            orderService.DeleteOrderByID(OrderID);
            return View();
        }


        [HttpPost()]
        public ActionResult UpdateOrder(Models.Order order,string[] ProductID,string[] UnitPrice , string[] Qty, string[] Discount)
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
            //訂單金額
            ViewBag.Total = 0;
            for (int i =0;i<ViewBag.OrderDetail.Count;i++)
            {
                if (ViewBag.OrderDetail[i].Discount != 0)
                {
                    ViewBag.Total += (ViewBag.OrderDetail[i].Qty * ViewBag.OrderDetail[i].UnitPrice) * ViewBag.OrderDetail[i].Discount;
                }else
                {
                    ViewBag.Total += (ViewBag.OrderDetail[i].Qty * ViewBag.OrderDetail[i].UnitPrice);
                }
            }
            return View();
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="CustomerName"></param>
        /// <param name="EmployeeName"></param>
        /// <param name="ShipperName"></param>
        /// <param name="OrderDate"></param>
        /// <param name="ShippedDate"></param>
        /// <param name="RequireDate"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult AjaxMethod(string OrderID, string CustomerName, string EmployeeName, string ShipperName, string OrderDate, string ShippedDate, string RequireDate)
        {
            Models.OrderService orderService = new Models.OrderService();
            Models.Order order = new Models.Order();
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
            List<Models.Order> data = orderService.AjaxGetOrderByCondition(order,"ASC");
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