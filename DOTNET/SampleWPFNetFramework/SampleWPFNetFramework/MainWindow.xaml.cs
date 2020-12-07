using System.Windows;
using MaterialDesignThemes;
using MaterialDesignColors;

namespace SampleWPFNetFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Datenbank01Entities db01Entities = new Datenbank01Entities();
        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
