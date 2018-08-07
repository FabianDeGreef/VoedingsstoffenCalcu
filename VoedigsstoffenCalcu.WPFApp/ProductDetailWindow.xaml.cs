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
using VoedingsstoffenCalcu.DomainClasses;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductDetailWindow : Window
    {
        private readonly Product _currentProduct;
        public ProductDetailWindow(Product product)
        {
            InitializeComponent();
            _currentProduct = new Product();
            _currentProduct = product;
            ViewProduct();
            ButtonSluit.Click += ButtonSluit_Click;
        }

        private void ButtonSluit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewProduct()
        {
            GridProduct.DataContext = _currentProduct;
        }
    }
}
