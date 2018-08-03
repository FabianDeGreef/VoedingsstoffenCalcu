using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using VoedingsstoffenCalcu.DomainClasses;

namespace VoedingsstoffenCalcu.Repository.Lite
{
    public class VoedingsstoffenContext : IDisposable
    {
        private LiteDatabase db;

        public LiteCollection<Product> Products { get; set; }
        public LiteCollection<DayEntry> DayEntrys { get; set; }
        public LiteCollection<Result> Results { get; set; }
        public LiteCollection<SavedProduct> SavedProducts { get; set; }

        public VoedingsstoffenContext()
        {
            bool insertDefaults = !File.Exists("Voedingstoffen.db");
            db = new LiteDatabase("Voedingstoffen.db");
            Products = db.GetCollection<Product>("Products");
            DayEntrys = db.GetCollection<DayEntry>("DayEntries");
            Results = db.GetCollection<Result>("Results");
            SavedProducts = db.GetCollection<SavedProduct>("SavedProducts");
            if (insertDefaults)
            {
                ImportExcel("VoedingsTabelV2.csv");
            }

        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool dis)
        {
            if (db != null)
            {
                db.Dispose();
            }
            if (dis)
            {
                GC.SuppressFinalize(this);
            }
        }
        public void ImportExcel(string file)
        {
            List<Product> productList = new List<Product>();
            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(';');
                        var product = new Product()
                        {
                            Naam = Convert.ToString(values[0]),
                            Kilocalorieën = Convert.ToDecimal(values[1]),
                            Kilojoules = Convert.ToDecimal(values[2]),
                            Water = Convert.ToDecimal(values[3]),
                            Eiwit = Convert.ToDecimal(values[4]),
                            Koolhydraten = Convert.ToDecimal(values[5]),
                            Suikers = Convert.ToDecimal(values[6]),
                            Vet = Convert.ToDecimal(values[7]),
                            VerzadigdVet = Convert.ToDecimal(values[8]),
                            EnkelvoudigOnverzadigdVet = Convert.ToDecimal(values[9]),
                            MeervoudigOnverzadigdeVetzuren = Convert.ToDecimal(values[10]),
                            Cholesterol = Convert.ToDecimal(values[11]),
                            Vezels = Convert.ToDecimal(values[12]),
                        };
                        productList.Add(product);
                    }
                }
            }
            Products.InsertBulk(productList);
        }
    }
}

