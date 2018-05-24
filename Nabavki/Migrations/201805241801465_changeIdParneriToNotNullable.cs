namespace Nabavki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIdParneriToNotNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner");
            DropForeignKey("dbo.Partner", "IdFirma", "dbo.Firma");
            DropIndex("dbo.Artikli", new[] { "IdPartner" });
            DropIndex("dbo.Partner", new[] { "IdFirma" });
            AlterColumn("dbo.Artikli", "IdPartner", c => c.Int(nullable: false));
            AlterColumn("dbo.Partner", "IdFirma", c => c.Int(nullable: false));
            CreateIndex("dbo.Artikli", "IdPartner");
            CreateIndex("dbo.Partner", "IdFirma");
            AddForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner", "IdPartner", cascadeDelete: true);
            AddForeignKey("dbo.Partner", "IdFirma", "dbo.Firma", "IdFirma", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partner", "IdFirma", "dbo.Firma");
            DropForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner");
            DropIndex("dbo.Partner", new[] { "IdFirma" });
            DropIndex("dbo.Artikli", new[] { "IdPartner" });
            AlterColumn("dbo.Partner", "IdFirma", c => c.Int());
            AlterColumn("dbo.Artikli", "IdPartner", c => c.Int());
            CreateIndex("dbo.Partner", "IdFirma");
            CreateIndex("dbo.Artikli", "IdPartner");
            AddForeignKey("dbo.Partner", "IdFirma", "dbo.Firma", "IdFirma");
            AddForeignKey("dbo.Artikli", "IdPartner", "dbo.Partner", "IdPartner");
        }
    }
}
