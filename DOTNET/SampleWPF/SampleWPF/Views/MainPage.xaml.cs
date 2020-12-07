using System.Windows.Controls;

using SampleWPF.ViewModels;

namespace SampleWPF.Views
{
    public partial class MainPage : Page
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
