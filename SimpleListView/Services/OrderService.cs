using System;
using System.Collections.Generic;
using SimpleListView.Models;
using System.Linq;

namespace SimpleListView.Services
{
	public class OrderService
	{
		List<Order> _ordersContainer;

		public OrderService ()
		{
			
			_ordersContainer = new List<Order> (FillContainer ());

		}

		private IEnumerable<Order> FillContainer ()
		{
			var random = new Random (0);
			var startDate = new DateTime (2016, 1, 1);

			for (int i = 0; i < 200; i++) {
				yield return new Order () {
					Id = i,
					ProductName = "Carlsberg 33cl",
					Quantity = random.Next (),
					Created = startDate.AddDays (i)
				};						
			}
		}

		public IEnumerable<Order> Load (DateTime? fromDate)
		{		
			if (!fromDate.HasValue)
				return _ordersContainer.OrderByDescending (order => order.Created).Take (20);

			if (!_ordersContainer.Any (o => o.Created < fromDate))
				return new List<Order> ();
			
			return _ordersContainer.Where (order => order.Created <= fromDate).OrderByDescending (order => order.Created).Take (20);
	
		}
	}
}

