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
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Globalization;

namespace SampleWPFNetFramework.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public static string DepotNameNew = String.Empty;
        public ModalWindow()
        {
            InitializeComponent();
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            DepotNameNew = txtSomeBox.Text;
            this.Close();
        }

        public ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = txtSomeBox.Text.Length;

            //try
            //{
            //    if (((string)value).Length > 0)
            //        age = Int32.Parse((String)value);
            //}
            //catch (Exception e)
            //{
            //    return new ValidationResult(false, $"Illegal characters or {e.Message}");
            //}

            if ((age < 5) || (age > 50))
            {
                return new ValidationResult(false,
                  $"Please enter an age in the range: 5-50.");
            }
            return ValidationResult.ValidResult;
        }

    }

}
