using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoedingsstoffenCalcu.DomainClasses;
using VoedingsstoffenCalcu.DomainModel;

namespace VoedingsstoffenCalcu.Repository
{
    public class Repository
    {
        public static List<Product> SearchProductContainsCharacters(string character)
        {
            using (var context  = new VoedingsstoffenContext())
            {
                try
                {
                    return context.Products.Where(s => s.Naam.ToLower().Contains(character.ToLower())).ToList();
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
                        products.Add(context.SavedProducts.FirstOrDefault(s => s.SavedProductId == id.SavedProductId));
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
                    return context.DayEntrys.Include("Result").Include("SavedProducts").FirstOrDefault(d => d.CurentDate.Day == date.Day && d.CurentDate.Month == date.Month && d.CurentDate.Year == date.Year);
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
                    context.DayEntrys.Add(dayEntryNew);
                    return context.SaveChanges();
                }
                catch (DbEntityValidationException e)
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
                    DayEntry dayEntry = context.DayEntrys.Include("Result").Include("SavedProducts").FirstOrDefault(w => w.DayEntryId == updateDayEntry.DayEntryId);
                    if (dayEntry != null)
                    {
                        dayEntry.SavedProducts.AddRange(updateDayEntry.SavedProducts);
                        dayEntry.Result = updateDayEntry.Result;
                        context.DayEntrys.AddOrUpdate(dayEntry);
                    }
                    return context.SaveChanges();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                catch (NullReferenceException e)
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
                    context.Products.AddOrUpdate(updateProduct);
                    return context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                catch (DbUpdateException e)
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
                    context.Products.Add(newProduct);
                    return context.SaveChanges();
                }
                catch (DbEntityValidationException e)
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
                    Product deleteProduct = context.Products.FirstOrDefault(w => w.ProductId == id);
                    if (deleteProduct != null) context.Products.Remove(deleteProduct);
                    return context.SaveChanges();
                }
                catch(ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
