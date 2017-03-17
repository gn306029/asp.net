using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderService
    {
        /// <summary>
        /// 新增訂單
        /// </summary>
        public void InsertOrder(Models.Class1 order)
        {

        }
        /// <summary>
        /// 刪除訂單 
        /// </summary>
        public void DeleteOrderByID()
        {

        }
        /// <summary>
        /// 修改訂單
        /// </summary>
        public void UpdateOrder()
        {

        }
        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Class1 GetOrderById(string id)
        {
            Models.Class1 result = new Class1();
            result.CustomerID = "123";
            result.CustomerName = "瑞陽資訊";
            return result;
        }
        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <returns></returns>
        public List<Models.Class1> GetOrders()
        {
            List<Models.Class1> result = new List<Class1>();
            result.Add(new Class1() { CustomerID = "GSS", CustomerName = "瑞陽資訊", EmployeeID = "1", EmployeeName = "路人甲" });
            result.Add(new Class1() { CustomerID = "NPOIS", CustomerName = "網軟資訊", EmployeeID = "2", EmployeeName = "路人乙" });
            return result;
        }
    }
}