using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.Repository;

namespace VoedigsstoffenCalcu.WPFApp
{
    public partial class ProductDayEntryWindow : Window
    {
        private ProductMessageWindow _message;
        private ProductMessageDialog _dialog;
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
            CalendarDatum.SelectedDatesChanged += DatePickerDatum_SelectedDatesChanged;
            ButtonExit.Click += ButtonExit_Click;
            ButtonRemoveDayEntry.Click += ButtonRemoveDayEntry_Click;
            LoadDayEntryFromCurrentDay();
        }

        private void ButtonRemoveDayEntry_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDayEntry != null)
            {
                if (Repository.DeleteCurrentDayEntry(_currentDayEntry) != 0)
                {
                    ResetDataContext();
                    DisplayMessage("Dag overzicht is verwijderd");
                }
                else
                {
                    DisplayMessage("Dag overzicht niet verwijderd");
                }
            }
            else
            {
                DisplayMessage("Geen dag overzicht gevonden op deze datum");
            }
        }

        private void DisplayMessage(string message)
        {
            _message = new ProductMessageWindow(message);
            _message.ShowDialog();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void DatePickerDatum_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarDatum.SelectedDate != null) _currentDateTime = (DateTime)CalendarDatum.SelectedDate;
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
                _dialog = new ProductMessageDialog();
                _dialog.ShowDialog();
                if (_dialog.DialogResult == true)
                {
                    DayEntry defaultDayEntry = new DayEntry()
                    {
                        CurentDate = Convert.ToDateTime(_currentDateTime.ToShortDateString()),
                        Result = new Result(),
                        SavedProducts = new List<SavedProduct>()                  
                    };
                    if (Repository.AddNewDayEntry(defaultDayEntry) != 0)
                    {
                        DisplayMessage("Nieuw dag overzicht aangemaakt");
                        LoadDayEntryFromCurrentDay();
                    }
                    else
                    {
                        DisplayMessage("Nieuw dag overzicht niet aangemaakt");
                    }
                }
                else
                {
                    DisplayMessage("Geen dag overzicht aangemaakt");
                }
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDayEntry != null)
            {
                var savedProducts = _currentDayEntry.SavedProducts;
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
                    DisplayMessage("Het dag overzicht is bijgewerkt");
                }
                else
                {
                    DisplayMessage("Het dag overzicht is niet bijgewerkt");
                }
                ButtonUpdate.IsEnabled = false;
                ListViewAdding.DataContext = null;
                LoadDayEntryFromCurrentDay();
            }
            else
            {
                DisplayMessage("Er is nog geen dag overzicht aangemaakt");
            }
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

        private void ListViewDagOverzicht_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var deleteItem = (SavedProduct)ListViewDagOverzicht.SelectedItem;
                List<SavedProduct> savedProducts = _currentDayEntry.SavedProducts;
                savedProducts.Remove(deleteItem);

                DayEntry dayEntryUpdate = new DayEntry()
                {
                    DayEntryId = _currentDayEntry.DayEntryId,
                    CurentDate = _currentDayEntry.CurentDate,
                    Result = Calculator.CalculateDayTotal(savedProducts),
                    SavedProducts = savedProducts,
                };
                if (Repository.DeleteAndRecalculateResultSavedProduct(dayEntryUpdate) != 0)
                {
                    DisplayMessage("Het geselecteerde product is verwijderd en het resultaat is herberekend");
                }
                else
                {
                    DisplayMessage("Het geselecteerde product en resultaat zijn niet gewijzigd");
                }
                LoadDayEntryFromCurrentDay();
            }
        }
    }
}
