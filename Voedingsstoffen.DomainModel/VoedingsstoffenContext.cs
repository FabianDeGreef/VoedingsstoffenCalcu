using System;
using System.IO;
using LiteDB;
using VoedingsstoffenCalcu.DomainClasses;

namespace Voedingsstoffen.DomainModel
{
    public class VoedingsstoffenContext : IDisposable
    {
        private LiteDatabase _database;

        public LiteCollection<Product> Products { get; set; }
        public LiteCollection<DayEntry> DayEntrys { get; set; }
        public LiteCollection<Result> Results { get; set; }
        public LiteCollection<SavedProduct> SavedProducts { get; set; }
        
        public VoedingsstoffenContext()
        {
            bool databaseExisit = !File.Exists("Voedingstoffen.db");
            CreateDatabase();
            Products = _database.GetCollection<Product>("Products");
            DayEntrys = _database.GetCollection<DayEntry>("DayEntries");
            Results = _database.GetCollection<Result>("Results");
            SavedProducts = _database.GetCollection<SavedProduct>("SavedProducts");
            if (databaseExisit)
            {
                InsertDefaults();
            }
        }

        private void InsertDefaults()
        {
            Products.InsertBulk(DataLoader.Loader("VoedingsTabelV2.csv"));
        }

        private void DeleteDatabase()
        {
            File.Delete("Voedingstoffen.db");

        }

        private void CreateDatabase()
        {
            _database = new LiteDatabase("Voedingstoffen.db");

        }

        public  void ResetDatabase()
        {
            DeleteDatabase();
            CreateDatabase();
            InsertDefaults();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool dis)
        {
            _database?.Dispose();
            if (dis)
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}

