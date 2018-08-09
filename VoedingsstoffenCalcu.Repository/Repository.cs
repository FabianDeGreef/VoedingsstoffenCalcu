using System;
using System.Collections.Generic;
using System.Linq;
using Voedingsstoffen.DomainModel;
using VoedingsstoffenCalcu.DomainClasses;

namespace VoedingsstoffenCalcu.Repository
{
    public static class Repository
    {
        public static List<Product> SearchProductContainsCharacters(string character)
        {
            using (var context = new VoedingsstoffenContext())
            {   
                return context.Products.Find(s => s.Naam.ToLower().Contains(character.ToLower())).ToList();
            }
        }

        public static DayEntry SearchDayEntryOnDate(DateTime date)
        {
            using (var context = new VoedingsstoffenContext())
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
        }

        public static int AddNewDayEntry(DayEntry dayEntryNew)
        {
            using (var context = new VoedingsstoffenContext())
            {
                return context.DayEntrys.Insert(dayEntryNew) != null ? 1 : 0;
            }
        }

        public static int UpdateExistingDayEntry(DayEntry updateDayEntry)
        {
            using (var context = new VoedingsstoffenContext())
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
        }

        public static int UpdateExistingProduct(Product updateProduct)
        {
            using (var context = new VoedingsstoffenContext())
            {
                return context.Products.Update(updateProduct) ? 1 : 0;
            }
        }

        public static int AddNewProduct(Product newProduct)
        {
            using (var context = new VoedingsstoffenContext())
            {
                return context.Products.Insert(newProduct);
            }
        }

        public static int DeleteExistingProduct(int id)
        {
            using (var context = new VoedingsstoffenContext())
            {
                return context.Products.Delete(w => w.ProductId == id);
            }
        }

        public static int DeleteCurrentDayEntry(DayEntry dayEntry)
        {
            using (var context = new VoedingsstoffenContext())
            {
                return context.DayEntrys.Delete(w => w.DayEntryId == dayEntry.DayEntryId);
            }
        }

        public static int DeleteAndRecalculateResultSavedProduct(DayEntry updateDayEntry)
        {
            using (var context = new VoedingsstoffenContext())
            {
                DayEntry dayEntry = context.DayEntrys.Include("Result").Include("SavedProducts").FindOne(w => w.DayEntryId == updateDayEntry.DayEntryId);
                if (dayEntry != null)
                {
                    dayEntry.SavedProducts = updateDayEntry.SavedProducts;
                    dayEntry.Result = updateDayEntry.Result;
                    return context.DayEntrys.Update(dayEntry) ? 1 : 0;
                }
                return 0;
            }
        }
    }
}

