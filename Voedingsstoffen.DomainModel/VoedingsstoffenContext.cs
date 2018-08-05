using System;
using System.IO;
using LiteDB;
using VoedingsstoffenCalcu.DomainClasses;

namespace Voedingsstoffen.DomainModel.Lite
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
                Products.InsertBulk(DataLoader.Loader("VoedingsTabelV2.csv"));
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
    }
}

