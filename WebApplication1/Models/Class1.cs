using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Class1
    {
        /// <summary>
        /// 
        /// </summary>
        public String OrderID { get; set; }
        public String CustomerID { get; set; }
        public String EmployeeID { get; set; }
        public String CustomerName { get; set; }
        public String EmployeeName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequireDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public String ShipperID { get; set; }
        public Double Freight { get; set; }
        public String ShipName { get; set; }
        public String ShipAddres { get; set; }
        public String ShipCity { get; set; }
        public String ShipRegion { get; set; }
        public String ShipPostalCode { get; set; }
        public String ShipCountry { get; set; }
    }
}