namespace DebtDestroyer.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        
        public INavigationViewModel NavigationViewModel { get; set; }

        public MainViewModel(INavigationViewModel navigationViewModel)
        {
            NavigationViewModel = navigationViewModel;
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }

    }
}
