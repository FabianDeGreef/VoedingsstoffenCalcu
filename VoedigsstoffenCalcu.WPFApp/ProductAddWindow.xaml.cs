using System.Windows;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductAddWindow : Window
    {
        private  Product _currentProduct;
        private ProductMessageWindow _message;

        public ProductAddWindow()
        {
            InitializeComponent();
            _currentProduct = new Product();
            GridProduct.DataContext = _currentProduct;
            ButtonVoegToe.Click += ButtonVoegToe_Click;
        }

        private void DisplayMessage(string message)
        {
            _message = new ProductMessageWindow(message);
            _message.ShowDialog();
        }

        private void ButtonVoegToe_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentProduct.Naam.Equals(""))
            {
                if (Repository.AddNewProduct(_currentProduct) != 0)
                {
                    DisplayMessage("Het product is toegevoegd");
                    ResetData();
                }
                else
                {
                    DisplayMessage("Het product is niet toegevoegd");
                }
            }
            else
            {
                DisplayMessage("Voer een naam in voor het nieuwe product");
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
