using System.Windows;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductAddWindow : Window
    {
        private  Product _currentProduct;
        public ProductAddWindow()
        {
            InitializeComponent();
            _currentProduct = new Product();
            GridProduct.DataContext = _currentProduct;
            ButtonVoegToe.Click += ButtonVoegToe_Click;
        }

        private void ButtonVoegToe_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentProduct.Naam.Equals(""))
            {
                if (Repository.AddNewProduct(_currentProduct) != 0)
                {
                    ProductMessageWindow message = new ProductMessageWindow("Het product is toegevoegd");
                    message.ShowDialog();
                    ResetData();
                }
                else
                {
                    ProductMessageWindow message = new ProductMessageWindow("Het product is niet toegevoegd");
                    message.ShowDialog();
                }
            }
            else
            {
                ProductMessageWindow message = new ProductMessageWindow("Voer een naam in voor het nieuwe product");
                message.ShowDialog();
            }
        }

        private void ResetData()
        {
            GridProduct.DataContext = null;
            _currentProduct = new Product();
            GridProduct.DataContext = _currentProduct;
        }
    }
}
