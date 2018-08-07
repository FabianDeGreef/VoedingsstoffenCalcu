using System.Windows;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductChangeWindow : Window
    {
        private Product _currentProduct;
        public ProductChangeWindow(Product product)
        {
            InitializeComponent();
            _currentProduct = new Product();
            _currentProduct = product;
            ViewProduct();
            ButtonUpdate.Click += ButtonUpdate_Click;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var product = _currentProduct;
            if (Repository.UpdateExistingProduct(_currentProduct) != 0)
            {
                ProductMessageWindow message = new ProductMessageWindow("Het product is gewijzigd");
                message.ShowDialog();
                this.Close();
            }
            else
            {
                ProductMessageWindow message = new ProductMessageWindow("Het product is niet gewijzigd");
                message.ShowDialog();
            }
        }

        private void ViewProduct()
        {
            GridProduct.DataContext = _currentProduct;
        }
    }
}
