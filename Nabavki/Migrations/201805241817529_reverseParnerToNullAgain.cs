namespace Nabavki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reverseParnerToNullAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Partner", "IdFirma", "dbo.Firma");
            DropForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner");
            DropIndex("dbo.Artikli", new[] { "IdPartner" });
            DropIndex("dbo.Partner", new[] { "IdFirma" });
            AlterColumn("dbo.Artikli", "IdPartner", c => c.Int(nullable: false));
            AlterColumn("dbo.Partner", "IdFirma", c => c.Int(nullable: false));
            CreateIndex("dbo.Artikli", "IdPartner");
            CreateIndex("dbo.Partner", "IdFirma");
            AddForeignKey("dbo.Partner", "IdFirma", "dbo.Firma", "IdFirma", cascadeDelete: true);
            AddForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner", "IdPartner", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner");
            DropForeignKey("dbo.Partner", "IdFirma", "dbo.Firma");
            DropIndex("dbo.Partner", new[] { "IdFirma" });
            DropIndex("dbo.Artikli", new[] { "IdPartner" });
            AlterColumn("dbo.Partner", "IdFirma", c => c.Int());
            AlterColumn("dbo.Artikli", "IdPartner", c => c.Int());
            CreateIndex("dbo.Partner", "IdFirma");
            CreateIndex("dbo.Artikli", "IdPartner");
            AddForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner", "IdPartner");
            AddForeignKey("dbo.Partner", "IdFirma", "dbo.Firma", "IdFirma");
        }
    }
}
