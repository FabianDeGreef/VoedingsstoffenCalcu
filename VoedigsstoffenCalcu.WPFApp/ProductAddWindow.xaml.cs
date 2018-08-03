using System.Windows;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository.Lite;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductAddWindow : Window
    {
        private  Product _currentProduct;
        public ProductAddWindow()
        {
            InitializeComponent();
            _currentProduct = new Product();
            StackPanelProduct.DataContext = _currentProduct;
            ButtonVoegToe.Click += ButtonVoegToe_Click;
        }

        private void ButtonVoegToe_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentProduct.Naam.Equals(""))
            {
                if (Repository.AddNewProduct(_currentProduct) != 0)
                {
                    MessageBox.Show("Het product is toegevoegd");
                    ResetData();
                }
                else
                {
                    MessageBox.Show("Het product is niet toegevoegd");
                }
            }
            else
            {
                MessageBox.Show("Voer een naam in voor het nieuwe product");
            }
        }

        private void ResetData()
        {
            StackPanelProduct.DataContext = null;
            _currentProduct = new Product();
            StackPanelProduct.DataContext = _currentProduct;
        }
    }
}
