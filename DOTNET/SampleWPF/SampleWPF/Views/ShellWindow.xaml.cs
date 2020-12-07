using System.Windows.Controls;

using MahApps.Metro.Controls;

using SampleWPF.Contracts.Views;
using SampleWPF.ViewModels;

namespace SampleWPF.Views
{
    public partial class ShellWindow : MetroWindow, IShellWindow
    {
        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public Frame GetRightPaneFrame()
            => rightPaneFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        public SplitView GetSplitView()
            => splitView;
    }
}
