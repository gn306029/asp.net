using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Class1
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        public String OrderID { get; set; }
        /// <summary>
        /// 客戶代號
        /// </summary>
        public String CustomerID { get; set; }
        public String EmployeeID { get; set; }
        public String CustomerName { get; set; }
        public String EmployeeName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequireDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public String ShipperID { get; set; }
        public String ShipperName { get; set; }
        public Decimal Freight { get; set; }
        public String ShipName { get; set; }
        public String ShipAddres { get; set; }
        public String ShipCity { get; set; }
        public String ShipRegion { get; set; }
        public String ShipPostalCode { get; set; }
        public String ShipCountry { get; set; }
    }
}