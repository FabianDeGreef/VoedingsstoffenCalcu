﻿using System.Windows;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductChangeWindow : Window
    {
        private Product _currentProduct;
        private ProductMessageWindow _message;
        public ProductChangeWindow(Product product)
        {
            InitializeComponent();
            _currentProduct = new Product();
            _currentProduct = product;
            ViewProduct();
            ButtonUpdate.Click += ButtonUpdate_Click;
        }

        private void DisplayMessage(string message)
        {
            _message = new ProductMessageWindow(message);
            _message.ShowDialog();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var product = _currentProduct;
            if (Repository.UpdateExistingProduct(_currentProduct) != 0)
            {
                DisplayMessage("Het product is gewijzigd");
                this.Close();
            }
            else
            {
                DisplayMessage("Het product is niet gewijzigd");
            }
        }

        private void ViewProduct()
        {
            GridProduct.DataContext = _currentProduct;
        }
    }
}
