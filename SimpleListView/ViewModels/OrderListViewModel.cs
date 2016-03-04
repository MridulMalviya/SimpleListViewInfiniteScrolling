using MvvmHelpers;
using SimpleListView.Services;
using SimpleListView.Models;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace SimpleListView.ViewModels
{
	public class OrderListViewModel : BaseViewModel
	{
		private OrderService _orderService = new OrderService ();
		private ObservableRangeCollection<Order> _orders = new ObservableRangeCollection<Order> ();

		private ICommand _refreshCommand, _loadMoreCommand = null;

		public OrderListViewModel ()
		{
			LoadOrders ();
		}

		public async Task LoadOrders ()
		{
			var newOrders = _orderService.Load (null);

			_orders.ReplaceRange (newOrders);

			Title = $"Orders {_orders.Count}";
		}

		public ObservableRangeCollection<Order> Orders {
			get{ return _orders; }

		}

		public ICommand RefreshCommand {
			get { return _refreshCommand ?? new Command (async () => await ExecuteRefreshCommand (), () => CanExecuteRefreshCommand ()); }
		}

		public bool CanExecuteRefreshCommand ()
		{			
			return IsNotBusy;
		}

		public async Task ExecuteRefreshCommand ()
		{   		
			IsBusy = true;

			await LoadOrders ();

			IsBusy = false;
		}

		public ICommand LoadMoreCommand {
			get { return _loadMoreCommand ?? new Command<Order> (ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand); }		
		}

		public bool CanExecuteLoadMoreCommand (Order item)
		{			
			return IsNotBusy && _orders.Count != 0 && _orders.OrderByDescending (o => o.Created).Last ().Created == item.Created;
		}

		public void ExecuteLoadMoreCommand (Order item)
		{   					
			IsBusy = true;
			var items = _orderService.Load (item.Created);
			_orders.AddRange (items);
			Title = $"Orders {_orders.Count}";
			IsBusy = false;
		}


	}
}

