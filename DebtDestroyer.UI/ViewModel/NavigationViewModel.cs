using DebtDestroyer.UI.DataProvider;
using System.Collections.ObjectModel;

namespace DebtDestroyer.UI.ViewModel
{
    public interface INavigationViewModel
    {
        void Load();
    }
    public class NavigationViewModel: ViewModelBase, INavigationViewModel
    {
        private readonly INavigationDataProvider _dataProvider;

        public NavigationViewModel(INavigationDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            //Friends = new ObservableCollection<NavigationItemViewModel>();
        }

        public ObservableCollection<NavigationItemViewModel> Customer { get; set; }
        public void Load()
        {
            //var customer = _dataProvider.[func avail];
           // foreach (var friend in friends)
            {
                Friends.Add(new NavigationItemViewModel 
                {
                    Id = friend.Id,
                    DisplayMember = friend.DisplayMember 
                });
            }
        }
    }
}
