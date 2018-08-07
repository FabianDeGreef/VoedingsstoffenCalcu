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

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductMessageDialog : Window
    {
        public ProductMessageDialog()
        {
            InitializeComponent();
            ButtonNo.Click += ButtonNo_Click;
            ButtonYes.Click += ButtonYes_Click;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
