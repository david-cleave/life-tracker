using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeTracker
{
    public class OrderManager2 : IOrderManager
    {
        public IEnumerable<Order> GetOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 1, CustomerName = "this is the second order manager", ShipperCity = "Smong", IsShipped = true }
            };

            return OrderList;
        }
    }
}