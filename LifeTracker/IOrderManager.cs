using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeTracker
{
    public interface IOrderManager
    {
        IEnumerable<Order> GetOrders();
    }
}