
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository.Lite;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductMainWindow : Window
    {
        private List<SavedProduct> _calculateProducts;
        public ProductMainWindow()
        {
            InitializeComponent();
            _calculateProducts = new List<SavedProduct>();
            ButtonSearch.Click += ButtonSearch_Click;
            ButtonEnter.Click += ButtonEnter_Click;
            ButtonReset.Click += ButtonReset_Click;
            ButtonDagTotaal.Click += ButtonDagTotaal_Click;
            MenuItemNieuwProduct.Click += MenuItemNieuwProduct_Click;
            MenuItemExit.Click += MenuItemExit_Click;
            MenuItemProductVerwijderen.Click += MenuItemProductVerwijderen_Click;
            MenuItemProductAanpassen.Click += MenuItemProductAanpassen_Click;
            VoedingsstoffenListView.SelectionChanged += VoedingsstoffenListView_SelectionChanged;
            VoedingsstoffenListView.MouseDoubleClick += VoedingsstoffenListView_MouseDoubleClick;

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }


        private void MenuItemProductAanpassen_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Product)VoedingsstoffenListView.SelectedItem;
            var aanpassenProduct = new ProductChangeWindow(selectedProduct);
            aanpassenProduct.Show();
            aanpassenProduct.Closed += AanpassenProduct_Closed;
        }

        private void MenuItemProductVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.DeleteExistingProduct(((Product)VoedingsstoffenListView.SelectedItem).ProductId) != 0)
            {
                MessageBox.Show("Het product is verwijderd");
            }
            else
            {
                MessageBox.Show("Het product is niet verwijderd");
            }
            ResetDataContextVoedingsstoffen();
            SearchOnName();
        }

        private void MenuItemNieuwProduct_Click(object sender, RoutedEventArgs e)
        {
            var productToevoegenWindow = new ProductAddWindow();
            productToevoegenWindow.Show();
        }

        private void ButtonDagTotaal_Click(object sender, RoutedEventArgs e)
        {
            var dagTotaalWindow = new ProductDayEntryWindow(_calculateProducts);
            dagTotaalWindow.Show();
            dagTotaalWindow.Closed += DagTotaalWindow_Closed;
        }

        private void VoedingsstoffenListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedProduct = (Product)VoedingsstoffenListView.SelectedItem;
            var detailProductWindow = new ProductDetailWindow(selectedProduct);
            detailProductWindow.Show();
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
                    var value = Convert.ToDecimal(TextBoxEnter.Text);
                    if (VoedingsstoffenListView.SelectedItem != null)
                    {
                        SavedProduct calculateProduct = Calculator.Calculate((Product)VoedingsstoffenListView.SelectedItem, value);
                        _calculateProducts.Add(calculateProduct);
                        ListViewCalcu.DataContext = _calculateProducts;
                        ResetTextboxes();
                        ResetDataContextVoedingsstoffen();
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Voer een decimaal of geheel getal in");
                    TextBoxEnter.Text = "";
                }
            }
        }

        private void SearchOnName()
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
            TextBoxEnter.Text = "";
        }

        private void ResetDataContextCalcu()
        {
            ListViewCalcu.DataContext = null;
        }
    }
}
