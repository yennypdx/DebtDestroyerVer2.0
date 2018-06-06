using System.Windows;
using System.Windows.Controls;
using DebtDestoyer.UI.ViewModel;

namespace DebtDestroyer.UI.View
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

        }

        //this is the method from the payment plan, to get prediction for so many months ahead
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
