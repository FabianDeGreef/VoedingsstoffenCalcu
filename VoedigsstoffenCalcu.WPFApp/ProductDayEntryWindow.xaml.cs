using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using VoedingsstoffenCalcu.Repository.Lite;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductDayEntryWindow : Window
    {
        private DateTime _currentDateTime;
        private DayEntry _currentDayEntry;
        private readonly List<SavedProduct> _currentProductList;

        public ProductDayEntryWindow(List<SavedProduct> productList)
        {
            InitializeComponent();
            _currentDateTime = DateTime.Now;
            _currentProductList = productList;
            PopulateAddingList();
            CheckForEmptyProList();
            ButtonUpdate.Click += ButtonUpdate_Click;
            DatePickerDatum.SelectedDateChanged += DatePickerDatum_SelectedDateChanged;
            LoadDayEntryFromCurrentDay();
        }

        private void PopulateAddingList()
        {
            ListViewAdding.DataContext = _currentProductList;
        }

        private void CheckForEmptyProList()
        {
            if (_currentProductList.Count == 0)
            {
                ButtonUpdate.IsEnabled = false;
            }
        }

        private void DatePickerDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDatum.SelectedDate != null) _currentDateTime = (DateTime) DatePickerDatum.SelectedDate;
            _currentDayEntry = null;
            LoadDayEntryFromCurrentDay();
        }

        private void LoadDayEntryFromCurrentDay()
        {
            _currentDayEntry = Repository.SearchDayEntryOnDate(_currentDateTime);
            if (_currentDayEntry != null)
            {
                CheckForEmptyProList();
                List<Result> result = new List<Result>();
                ListViewDagOverzicht.DataContext = _currentDayEntry.SavedProducts;
                var resultOne = _currentDayEntry.Result;
                result.Add(resultOne);
                ListViewResult.DataContext = result;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Voor de huidige datum worden nu nieuwe gegevens aangemaakt", "Gegevens aanmaken voor " + _currentDateTime.ToShortDateString(), MessageBoxButton.OK, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.OK)
                {
                    DayEntry defaultDayEntry = new DayEntry()
                    {
                        CurentDate = Convert.ToDateTime(_currentDateTime.ToShortDateString()),
                        Result = new Result(),
                        SavedProducts = new List<SavedProduct>()                  
                    };
                    if (Repository.AddNewDayEntry(defaultDayEntry) != 0)
                    {
                        MessageBox.Show("Nieuw dag overzicht aangemaakt");
                        LoadDayEntryFromCurrentDay();
                    }
                    else
                    {
                        MessageBox.Show("Nieuw dag overzicht niet aangemaakt");
                    }
                }
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<SavedProduct> savedProducts = new List<SavedProduct>();
            savedProducts = _currentDayEntry.SavedProducts;
            savedProducts.AddRange(_currentProductList);

            foreach (var t in savedProducts)
            {
                t.DayEntryId = _currentDayEntry.DayEntryId;
            }

            DayEntry dayEntryUpdate = new DayEntry()
            {
                DayEntryId = _currentDayEntry.DayEntryId,
                CurentDate = _currentDayEntry.CurentDate,
                Result = Calculator.CalculateDayTotal(savedProducts),
                SavedProducts = _currentProductList

            };
            if (Repository.UpdateExistingDayEntry(dayEntryUpdate) != 0)
            {
                MessageBox.Show("Het dag overzicht is bijgewerkt");
            }
            else
            {
                MessageBox.Show("Het dag overzicht is niet bijgewerkt");
            }
            ButtonUpdate.IsEnabled = false;
            ListViewAdding.DataContext = null;
            LoadDayEntryFromCurrentDay();
        }

        private void ResetDataContext()
        {
            ListViewDagOverzicht.DataContext = null;
            ListViewResult.DataContext = null;
        }

        private void ListViewDagOverzicht_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedSavedProduct = (SavedProduct)ListViewDagOverzicht.SelectedItem;
            var product = new Product()
            {
                Cholesterol = selectedSavedProduct.Cholesterol,
                ProductId = selectedSavedProduct.OrginalProductId,
                Eiwit = selectedSavedProduct.Eiwit,
                EnkelvoudigOnverzadigdVet = selectedSavedProduct.EnkelvoudigOnverzadigdVet,
                Kilocalorieën = selectedSavedProduct.Kilocalorieën,
                Kilojoules = selectedSavedProduct.Kilojoules,
                Koolhydraten = selectedSavedProduct.Koolhydraten,
                MeervoudigOnverzadigdeVetzuren = selectedSavedProduct.MeervoudigOnverzadigdeVetzuren,
                Naam = selectedSavedProduct.Naam,
                Suikers = selectedSavedProduct.Suikers

            };
            var detailProductWindow = new ProductDetailWindow(product);
            detailProductWindow.Show();
        }
    }
}
