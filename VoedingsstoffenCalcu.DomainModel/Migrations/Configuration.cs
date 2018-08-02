using System.Collections.Generic;
using VoedingsstoffenCalcu.DomainClasses;

namespace VoedingsstoffenCalcu.DomainModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VoedingsstoffenCalcu.DomainModel.VoedingsstoffenContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AppDomain.CurrentDomain.SetData("DataDirectory", "C:Users/Fabian/Documents/Visual Studio 2017/Projects/VoedingsstoffenCalcu/VoedigsstoffenCalcu.WPFApp/VoedingsstoffenDb.mdf");

        }

        protected override void Seed(VoedingsstoffenCalcu.DomainModel.VoedingsstoffenContext context)
        {
            List<Product> products = DataLoader.Loader();
            context.Products.AddRange(products);
        }
    }
}
