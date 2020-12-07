using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls.Primitives;
using SampleWPFNetFramework.View;

namespace SampleWPFNetFramework.Views
{
    /// <summary>
    /// Interaction logic for DepotView.xaml
    /// </summary>
    public partial class DepotView : UserControl
    {
        Datenbank01Entities db01Entities = new Datenbank01Entities();

        public DepotView()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<V_UserDepot> UserDepotData = await db01Entities.V_UserDepot.Where(x => x.UserName.ToUpper() == "JULIAN-FRECH@GMX.DE").ToListAsync();//.Select(x => new { x.DepotName, UserName = x.UserName.ToLower() , x.Company, x.Quantity})

            dataGrid1.ItemsSource = UserDepotData;
        }

        private void btnOpenModal_Click(object sender, RoutedEventArgs e)
        {
            ModalWindow modalWindow = new ModalWindow();
            modalWindow.ShowDialog();

            string valueFromModalTextBox = ModalWindow.DepotNameNew;


        }
    }
}
