
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Voedingsstoffen.DomainModel;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductMainWindow : Window
    {
        private List<SavedProduct> _calculateProducts;
        private ProductMessageWindow _message;
        private decimal _weight;

        public ProductMainWindow()
        {
            InitializeComponent();           
            _calculateProducts = new List<SavedProduct>();
            ButtonSearch.Click += ButtonSearch_Click;
            ButtonEnter.Click += ButtonEnter_Click;
            ButtonReset.Click += ButtonReset_Click;
            ButtonDagTotaal.Click += ButtonDagTotaal_Click;
            VoedingsstoffenListView.SelectionChanged += VoedingsstoffenListView_SelectionChanged;
            VoedingsstoffenListView.MouseDoubleClick += VoedingsstoffenListView_MouseDoubleClick;
            MenuItemResetDatabase.Click += MenuItemResetDatabase_Click;
            MenuItemNieuwProduct.Click += MenuItemNieuwProduct_Click;
            MenuItemExit.Click += MenuItemExit_Click;
            MenuItemProductVerwijderen.Click += MenuItemProductVerwijderen_Click;
            MenuItemProductAanpassen.Click += MenuItemProductAanpassen_Click;
            MenuItemInfo.Click += MenuItemInfo_Click;
            TextBoxEnter.PreviewTextInput += TextBoxEnter_PreviewTextInput;
            TextBoxSearch.PreviewTextInput += TextBoxSearch_PreviewTextInput;
            ButtonSluit.Click += ButtonSluit_Click;
        }

        private void ButtonSluit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DisplayMessage(string message)
        {
            _message = new ProductMessageWindow(message);
            _message.ShowDialog();
        }

        private void TextBoxSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!(Char.IsLetter(ch)))
                {
                    e.Handled = true;
                }
            }
        }

        private void TextBoxEnter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!(Char.IsDigit(ch) || ch.Equals(',')))
                {
                    e.Handled = true;
                }
            }              
        }

        private void MenuItemInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplayMessage("© Copyright 2018. Alle rechten voorbehouden");
        }

        private void MenuItemResetDatabase_Click(object sender, RoutedEventArgs e)
        {
            VoedingsstoffenContext v  = new VoedingsstoffenContext();
            v.ResetDatabase();
            DisplayMessage("Database opnieuw aangemaakt");
            SearchOnName();

        }

        private void MenuItemProductAanpassen_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Product)VoedingsstoffenListView.SelectedItem;
            if (!Application.Current.Windows.OfType<ProductChangeWindow>().Any())
            {
                var aanpassenProduct = new ProductChangeWindow(selectedProduct);
                aanpassenProduct.Show();
                aanpassenProduct.Closed += AanpassenProduct_Closed;

            }
        }

        private void DeleteProduct()
        {
            if (Repository.DeleteExistingProduct(((Product)VoedingsstoffenListView.SelectedItem).ProductId) != 0)
            {
                DisplayMessage("Het product is verwijderd");
                ResetWeightTextBox();
            }
            else
            {
                DisplayMessage("Het product is niet verwijderd");
            }
            ResetDataContextVoedingsstoffen();
            SearchOnName();
        }

        private void MenuItemProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            DeleteProduct();
        }

        private void MenuItemNieuwProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ProductAddWindow>().Any())
            {
                var productToevoegenWindow = new ProductAddWindow();
                productToevoegenWindow.Show();
            }
        }

        private void ButtonDagTotaal_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ProductDayEntryWindow>().Any())
            {
                var dagTotaalWindow = new ProductDayEntryWindow(_calculateProducts);
                dagTotaalWindow.Show();
                dagTotaalWindow.Closed += DagTotaalWindow_Closed;
            }
        }

        private void VoedingsstoffenListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ProductDetailWindow>().Any())
            {
                var detailProductWindow = new ProductDetailWindow((Product) VoedingsstoffenListView.SelectedItem);
                detailProductWindow.Show();
            }
        }

        private void VoedingsstoffenListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VoedingsstoffenListView.SelectedItem != null)
            {
                ButtonEnter.IsEnabled = true;
                TextBoxEnter.IsEnabled = true;
                MenuItemProductAanpassen.IsEnabled = true;
                MenuItemProductVerwijderen.IsEnabled = true;
            }
            else
            {
                ButtonEnter.IsEnabled = false;
                TextBoxEnter.IsEnabled = false;
                MenuItemProductAanpassen.IsEnabled = false;
                MenuItemProductVerwijderen.IsEnabled = false;
            }
        }

        private void EnterAmount()
        {
            if (!TextBoxEnter.Text.Equals(""))
            {
                try
                {
                    ResetDataContextCalcu();
                    _weight = Convert.ToDecimal(TextBoxEnter.Text);
                    if (VoedingsstoffenListView.SelectedItem != null)
                    {
                        SavedProduct calculateProduct = Calculator.Calculate((Product)VoedingsstoffenListView.SelectedItem, _weight);
                        _calculateProducts.Add(calculateProduct);
                        ListViewCalcu.DataContext = _calculateProducts;
                        ResetTextboxes();
                        ResetDataContextVoedingsstoffen();
                    }
                }
                catch (FormatException)
                {
                    DisplayMessage("Voer een decimaal of geheel getal in");
                    ResetWeightTextBox();
                }
            }
        }

        private void ResetWeightTextBox()
        {
            TextBoxEnter.Text = "";
        }

        private void SearchOnName()
        {
            if (!TextBoxSearch.Text.Equals(""))
            {
                string search = TextBoxSearch.Text;
                var products = Repository.SearchProductContainsCharacters(search);
                if (products.Count != 0)
                {
                    VoedingsstoffenListView.DataContext = products;
                }
            }
        }

        private void ListViewCalcu_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var deleteProduct = ((SavedProduct)ListViewCalcu.SelectedItem);
                _calculateProducts.Remove(deleteProduct);
                ResetDataContextCalcu();
                ListViewCalcu.DataContext = _calculateProducts;
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchOnName();
        }


        private void TextBoxSearch_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchOnName();
            }
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            EnterAmount();
        }

        private void TextBoxEnter_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterAmount();
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DagTotaalWindow_Closed(object sender, EventArgs e)
        {
            Reset();
        }

        private void AanpassenProduct_Closed(object sender, EventArgs e)
        {
            Reset();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            ResetTextboxes();
            _calculateProducts.Clear();
            ResetDataContextCalcu();
            ResetDataContextVoedingsstoffen();
        }

        private void ResetDataContextVoedingsstoffen()
        {
            VoedingsstoffenListView.DataContext = null;
        }

        private void ResetTextboxes()
        {
            TextBoxSearch.Text = "";
            ResetWeightTextBox();
        }

        private void ResetDataContextCalcu()
        {
            ListViewCalcu.DataContext = null;
        }

        private void VoedingsstoffenListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteProduct();
            }
        }
    }
}
