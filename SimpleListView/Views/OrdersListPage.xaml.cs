using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SimpleListView.ViewModels;

namespace SimpleListView.Views
{
	public partial class OrdersListPage : ContentPage
	{
		public OrdersListPage ()
		{
			InitializeComponent ();

			BindingContext = new OrderListViewModel ();
		}
	}
}

