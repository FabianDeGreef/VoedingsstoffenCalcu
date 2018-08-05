using System;
using System.Collections.Generic;
using System.Linq;
using Voedingsstoffen.DomainModel.Lite;
using VoedingsstoffenCalcu.DomainClasses;

namespace VoedingsstoffenCalcu.Repository.Lite
{
    public static class Repository
    {
        public static List<Product> SearchProductContainsCharacters(string character)
        {
            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    return context.Products.Find(s => s.Naam.ToLower().Contains(character.ToLower())).ToList();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static List<SavedProduct> GetSavedProductByIds(List<SavedProduct> productIds)
        {
            List<SavedProduct> products = new List<SavedProduct>();

            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    foreach (var id in productIds)
                    {
                        products.Add(context.SavedProducts.FindOne(s => s.SavedProductId == id.SavedProductId));
                    }
                    return products;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static DayEntry SearchDayEntryOnDate(DateTime date)
        {
            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    foreach (var x in context.DayEntrys.FindAll())
                    {
                        if (x.CurentDate.Day == date.Day && x.CurentDate.Month == date.Month && x.CurentDate.Year == date.Year)
                        {
                            return x;
                        }
                    }
                    return null;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static int AddNewDayEntry(DayEntry dayEntryNew)
        {
            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    return context.DayEntrys.Insert(dayEntryNew) != null ? 1 : 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }

        public static int UpdateExistingDayEntry(DayEntry updateDayEntry)
        {
            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    DayEntry dayEntry = context.DayEntrys.Include("Result").Include("SavedProducts").FindOne(w => w.DayEntryId == updateDayEntry.DayEntryId);
                    if (dayEntry != null)
                    {
                        dayEntry.SavedProducts.AddRange(updateDayEntry.SavedProducts);
                        dayEntry.Result = updateDayEntry.Result;
                        return context.DayEntrys.Update(dayEntry) ? 1 : 0;
                    }
                    return 0;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static int UpdateExistingProduct(Product updateProduct)
        {
            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    return context.Products.Update(updateProduct) ? 1 : 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static int AddNewProduct(Product newProduct)
        {
            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    return context.Products.Insert(newProduct);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static int DeleteExistingProduct(int id)
        {
            using (var context = new VoedingsstoffenContext())
            {
                try
                {
                    return context.Products.Delete(w => w.ProductId == id);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}

