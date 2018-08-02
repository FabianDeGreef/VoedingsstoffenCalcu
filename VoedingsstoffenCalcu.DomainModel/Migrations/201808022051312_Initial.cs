namespace VoedingsstoffenCalcu.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayEntries",
                c => new
                    {
                        DayEntryId = c.Int(nullable: false, identity: true),
                        CurentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DayEntryId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Int(nullable: false),
                        Kilocalorieën = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Kilojoules = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Water = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Eiwit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Koolhydraten = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Suikers = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VerzadigdVet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnkelvoudigOnverzadigdVet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeervoudigOnverzadigdeVetzuren = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cholesterol = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vezels = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.DayEntries", t => t.ResultId)
                .Index(t => t.ResultId);
            
            CreateTable(
                "dbo.SavedProducts",
                c => new
                    {
                        SavedProductId = c.Int(nullable: false, identity: true),
                        OrginalProductId = c.Int(nullable: false),
                        DayEntryId = c.Int(),
                        Naam = c.String(),
                        Kilocalorieën = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Kilojoules = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Water = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Eiwit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Koolhydraten = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Suikers = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VerzadigdVet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnkelvoudigOnverzadigdVet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeervoudigOnverzadigdeVetzuren = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cholesterol = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vezels = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SavedProductId)
                .ForeignKey("dbo.DayEntries", t => t.DayEntryId)
                .Index(t => t.DayEntryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Kilocalorieën = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Kilojoules = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Water = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Eiwit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Koolhydraten = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Suikers = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VerzadigdVet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnkelvoudigOnverzadigdVet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeervoudigOnverzadigdeVetzuren = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cholesterol = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vezels = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedProducts", "DayEntryId", "dbo.DayEntries");
            DropForeignKey("dbo.Results", "ResultId", "dbo.DayEntries");
            DropIndex("dbo.SavedProducts", new[] { "DayEntryId" });
            DropIndex("dbo.Results", new[] { "ResultId" });
            DropTable("dbo.Products");
            DropTable("dbo.SavedProducts");
            DropTable("dbo.Results");
            DropTable("dbo.DayEntries");
        }
    }
}
