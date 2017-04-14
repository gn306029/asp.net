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
            string sql = @" Insert Into Sales.Orders
                            (
                                CustomerID,
                                EmployeeID,
                                OrderDate,
                                RequireDdate,
                                ShippedDate,
                                ShipperId,
                                Freight,
                                ShipName,
                                ShipAddress,
                                ShipCity,
                                ShipRegion,
                                ShipPostalCode,
                                ShipCountry
                            )
                            Values
                            (
                                @CustomerID,
                                @EmployeeID,
                                @OrderDate,
                                @RequireDdate,
                                @ShippedDate,
                                @ShipperId,
                                @Freight,
                                @ShipName,
                                @ShipAddress,
                                @ShipCity,
                                @ShipRegion,
                                @ShipPostalCode,
                                @ShipCountry
                            )
                            Select SCOPE_IDENTITY()
                            ";
            Boolean test = false;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", Convert.ToInt32(order.CustomerID)));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", Convert.ToInt32(order.EmployeeID)));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@RequireDdate", order.RequireDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@ShipperId", Convert.ToInt32(order.ShipperID)));
                cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                cmd.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddres));
                cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                if (cmd.ExecuteScalar().Equals("1"))//測試用
                {
                    test = true;
                }
                conn.Close();
            }
        }
        /// <summary>
        /// 刪除訂單 
        /// </summary>
        public void DeleteOrderByID(string OrderID)
        {
            string sql = "Delete From Sales.Orders Where OrderID = @OrderID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID",OrderID));
                cmd.ExecuteScalar();
                
                conn.Close();
            }
        }
        /// <summary>
        /// 修改訂單
        /// </summary>
        public void UpdateOrder(string OrderID,Class1 order)
        {
            string sql = @"Update Sales.Orders set 
                            CustomerID = @CustomerID,EmployeeID = @EmployeeID,
                            OrderDate = @OrderDate,RequiredDate = @RequireDate,
                            ShippedDate = @ShippedDate,ShipperID = @ShipperID,
                            Freight = @Freight,ShipName = @ShipName,
                            ShipAddress = @ShipAddress,ShipCity = @ShipCity,
                            ShipRegion = @ShipRegion,ShipPostalCode = @ShipPostalCode,ShipCountry = @ShipCountry
                            Where OrderID = @OrderID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                cmd.Parameters.Add(new SqlParameter("@CustomerID", Convert.ToInt32(order.CustomerID)));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", Convert.ToInt32(order.EmployeeID)));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@Requiredate", order.RequireDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@ShipperId", Convert.ToInt32(order.ShipperID)));
                cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                cmd.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddres));
                cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                cmd.ExecuteScalar();
                conn.Close();
            }
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

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
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

        private List<Models.Class1> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Class1> result = new List<Class1>();

            foreach (DataRow row in orderData.Rows)
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
