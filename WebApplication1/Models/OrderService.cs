using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class OrderService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
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
        /// 依照id取得訂單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Class1 GetOrderById(string orderid)
        {
            DataTable dt = new DataTable();
            string sql = @"Select
                    A.OrderId,A.CustomerID,B.Companyname As CustName,
                    A.EmployeeID,C.lastname+C.firstname As EmpName,
                    A.OrderDate,A.RequireDdate,A.ShippedDate,
                    A.ShipperId,D.companyname As ShipperName,A.Freight,
                    A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry
                    From Sales.Orders As A
                    INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
                    INNER JOIN HR.Employees As C ON A.EmployeeID=C.EmployeeID
                    INNER JOIN Sales.Shippers As D ON A.shipperid=D.shipperid
                    Where A.OrderID=@OrderID";

            using(SqlConnection conn=new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", orderid));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapOrderDataToList(dt).FirstOrDefault();
        }
        /// <summary>
        /// 依需求日期由近到遠取得訂單
        /// </summary>
        /// <returns></returns>
        public List<Models.Class1> GetOrderByRequireDate()
        {
            List<Models.Class1> result = new List<Class1>();
            DataTable dt = new DataTable();
            string sql = @"Select Top 10 with ties
                    A.OrderId,A.CustomerID,B.Companyname As CustName,
                    A.EmployeeID,C.lastname+C.firstname As EmpName,
                    A.OrderDate,A.RequireDdate,A.ShippedDate,
                    A.ShipperId,D.companyname As ShipperName,A.Freight,
                    A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry
                    From Sales.Orders As A
                    INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
                    INNER JOIN HR.Employees As C ON A.EmployeeID=C.EmployeeID
                    INNER JOIN Sales.Shippers As D ON A.shipperid=D.shipperid
					Order by RequiredDate";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            result = MapOrderDataToList(dt);
            return result;
        }

        private List<Models.Class1>MapOrderDataToList(DataTable orderData)
        {
            List<Models.Class1> result = new List<Class1>();

            foreach(DataRow row in orderData.Rows)
            {
                result.Add(new Class1()
                {
                    CustomerID = row["CustomerID"].ToString(),
                    CustomerName = row["CustName"].ToString(),
                    EmployeeID = row["EmployeeID"].ToString(),
                    EmployeeName = row["EmpName"].ToString(),
                    Freight = (decimal)row["Freight"],
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["OrderDate"],
                    OrderID = row["OrderId"].ToString(),
                    RequireDate = row["RequireDdate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequireDdate"],
                    ShipAddres = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString(),
                    ShipName = row["ShipName"].ToString(),
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    ShipperID = row["ShipperId"].ToString(),
                    ShipperName = row["ShipperName"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString()
                });
            }
            return result;
        }
    }
}
