using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

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
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                cmd.ExecuteScalar();

                conn.Close();
            }
        }
        /// <summary>
        /// 修改訂單
        /// </summary>
        public void UpdateOrder(string OrderID, Class1 order, string[] ProductID, string[] UnitPrice, string[] Qty, string[] Discount)
        {
            string sql_detail = @"Delete From Sales.OrderDetails Where OrderID = @OrderID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql_detail, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                cmd.ExecuteScalar();
                conn.Close();
            }
            for (int i = 0; i < ProductID.Length; i++)
            {
                string sql_insertdetail = @"Insert Into Sales.OrderDetails(OrderID,ProductID,UnitPrice,Qty,Discount) Values (@OrderID,@ProductID,@UnitPrice,@Qty,@Discount)";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(sql_insertdetail, conn);
                        cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                        cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID[i]));
                        cmd.Parameters.Add(new SqlParameter("@UnitPrice", UnitPrice[i]));
                        cmd.Parameters.Add(new SqlParameter("@Qty", Qty[i]));
                        cmd.Parameters.Add(new SqlParameter("@Discount", Discount[i]));
                        cmd.ExecuteScalar();
                        conn.Close();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
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
                cmd.Parameters.Add(new SqlParameter("@CustomerID", Convert.ToInt32(order.CustomerID == null ? "" : order.CustomerID)));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", Convert.ToInt32(order.EmployeeID == null ? "" : order.EmployeeID)));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@Requiredate", order.RequireDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@ShipperId", Convert.ToInt32(order.ShipperID == null ? "" : order.ShipperID)));
                cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                cmd.Parameters.Add(new SqlParameter("@ShipName", order.ShipName == null ? "" : order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddres == null ? "" : order.ShipAddres));
                cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity == null ? "" : order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion == null ? "" : order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode == null ? "" : order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry == null ? "" : order.ShipPostalCode));
                cmd.ExecuteScalar();
                conn.Close();
            }
        }
        public List<Models.Class1> GetOrderDetail(string orderID)
        {
            DataTable dt = new DataTable();
            string sql = @"Select * From Sales.OrderDetails Where OrderID = @orderID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@OrderId", orderID));

                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                    sqlAdapter.Fill(dt);
                    conn.Close();
                }
                catch (Exception e)
                {

                }
            }
            return this.MapOrderDataToList_2(dt);
        }
        private List<Models.Class1> MapOrderDataToList_2(DataTable orderData)
        {
            List<Models.Class1> result = new List<Class1>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Class1()
                {
                    OrderID = row["OrderId"].ToString() == null ? "" : row["OrderId"].ToString(),
                    ProductID = row["ProductID"].ToString() == null ? "" : row["ProductID"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"] == null ? "0" : row["UnitPrice"]),
                    Qty = Convert.ToInt32(row["Qty"] == null ? "0" : row["Qty"]),
                    Discount = Convert.ToDecimal(row["Discount"] == null ? "0" : row["Discount"])
                });
            }
            return result;
        }
        /// <summary>
        /// 取的該筆訂單明細的產品名稱
        /// </summary>
        /// <returns></returns>
        public List<string> GetOrderProductName(string OrderID)
        {
            List<string> OrderProductName = new List<string>();
            string sql = @"Select ProductName From Sales.OrderDetails as a Join Production.Products as b  on a.ProductID = b.ProductID Where a.OrderID = @OrderID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        OrderProductName.Add(sqlDataReader[0].ToString());
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return OrderProductName;
        }
        /// <summary>
        /// 取得產品ID
        /// </summary>
        /// <returns></returns>
        public List<string> GetProductID()
        {
            List<string> ProductID = new List<string>();
            string sql = @"Select ProductID From Production.Products Order By ProductID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        ProductID.Add(sqlDataReader[0].ToString());
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return ProductID;
        }
        /// <summary>
        /// 取得產品名稱
        /// </summary>
        /// <returns></returns>
        public List<string> GetProductName()
        {
            List<string> ProductName = new List<string>();
            string sql = @"Select ProductName From Production.Products Order By ProductID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        ProductName.Add(sqlDataReader[0].ToString());
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return ProductName;
        }
        /// <summary>
        /// 取得員工名稱
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetOrderEmployee()
        {
            List<SelectListItem> Employee_name = new List<SelectListItem>();
            string sql = @"Select lastname+firstname From HR.Employees Order By EmployeeID";
            Employee_name.Add(new SelectListItem { Text = "------", Value = "" });
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Employee_name.Add(new SelectListItem { Text = sqlDataReader[0].ToString(), Value = sqlDataReader[0].ToString() });
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return Employee_name;
        }
        /// <summary>
        /// 取得貨運公司
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetOrderShipper()
        {

            List<SelectListItem> Shipper = new List<SelectListItem>();
            string sql = @"Select companyname From Sales.Shippers Order By ShipperID";
            Shipper.Add(new SelectListItem { Text = "------", Value = "" });
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Shipper.Add(new SelectListItem { Text = sqlDataReader[0].ToString(), Value = sqlDataReader[0].ToString() });
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return Shipper;
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
        /// <summary>
        /// Ajax依OrderID做排序
        /// A_D 為 ASC 或 DESC
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<Models.Class1> AjaxGetOrderByCondition(Models.Class1 order,string A_D)
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
                    Where A.OrderID Like '%'+@OrderID+'%'
                    And B.Companyname Like '%'+@CustomerName+'%'
                    And (C.LastName+FirstName) Like '%'+@EmployeeName+'%' And D.CompanyName Like '%'+@ShipperName+'%' 
                    And A.OrderDate Like '%'+@OrderDate+'%'
                    And A.RequiredDate Like '%'+@RequireDate+'%'
                    And A.ShippedDate Like '%'+@ShippedDate+'%'
                    Order By A.OrderID ASC
                    ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", order.OrderID));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", order.CustomerName == null ? "" : order.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@EmployeeName", order.EmployeeName == null ? "" : order.EmployeeName));
                cmd.Parameters.Add(new SqlParameter("@ShipperName", order.ShipperName == null ? "" : order.ShipperName));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate == null ? "" : order.OrderDate + ""));
                cmd.Parameters.Add(new SqlParameter("@RequireDate", order.RequireDate == null ? "" : order.RequireDate + ""));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate == null ? "" : order.ShippedDate + ""));
                //cmd.Parameters.Add(new SqlParameter("@A_D", A_D));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapOrderDataToList(dt);
        }
        /// <summary>
        /// 依照條件取得訂單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Models.Class1> GetOrderByCondition(Models.Class1 order)
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
                    Where A.OrderID Like '%'+@OrderID+'%'
                    And B.Companyname Like '%'+@CustomerName+'%'
                    And (C.LastName+FirstName) Like '%'+@EmployeeName+'%' And D.CompanyName Like '%'+@ShipperName+'%' 
                    And A.OrderDate Like '%'+@OrderDate+'%'
                    And A.RequiredDate Like '%'+@RequireDate+'%'
                    And A.ShippedDate Like '%'+@ShippedDate+'%'
                    ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", order.OrderID));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", order.CustomerName == null ? "" : order.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@EmployeeName", order.EmployeeName == null ? "" : order.EmployeeName));
                cmd.Parameters.Add(new SqlParameter("@ShipperName", order.ShipperName == null ? "" : order.ShipperName));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate == null ? "" : order.OrderDate + ""));
                cmd.Parameters.Add(new SqlParameter("@RequireDate", order.RequireDate == null ? "" : order.RequireDate + ""));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate == null ? "" : order.ShippedDate + ""));
                /*cmd.Parameters.Add(new SqlParameter("@OrderId", order.OrderID));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", order.CustomerName == null ? "%%" : "%"+order.CustomerName+"%"));
                cmd.Parameters.Add(new SqlParameter("@EmployeeName", order.EmployeeName));
                cmd.Parameters.Add(new SqlParameter("@ShipperName", order.ShipperName));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate== null ? "%%" : "%"+order.OrderDate+"%"));
                cmd.Parameters.Add(new SqlParameter("@RequireDate", order.RequireDate == null ? "%%" : "%"+order.RequireDate+"%"));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate == null ? "%%" : "%"+order.ShippedDate+"%"));*/
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapOrderDataToList(dt);
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
