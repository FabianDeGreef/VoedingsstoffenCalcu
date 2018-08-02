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
                MessageBox.Show("Het product is gewijzigd");
                this.Close();
            }
            else
            {
                MessageBox.Show("Het product is niet gewijzigd");
            }
        }

        private void ViewProduct()
        {
            StackPanelProduct.DataContext = _currentProduct;
        }
    }
}
