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
    /// <summary>
    /// Interaction logic for ProductDetailWindow.xaml
    /// </summary>
    public partial class ProductDetailWindow : Window
    {
        private readonly Product _currentProduct;
        public ProductDetailWindow(Product product)
        {
            InitializeComponent();
            _currentProduct = new Product();
            _currentProduct = product;
            ViewProduct();
        }

        private void ViewProduct()
        {
            StackPanelProduct.DataContext = _currentProduct;
        }
    }
}
