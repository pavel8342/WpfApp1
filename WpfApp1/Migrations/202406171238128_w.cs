namespace WpfApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.города",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        город = c.String(nullable: false),
                        UserType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserTypes", t => t.UserType_ID)
                .Index(t => t.UserType_ID);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.история",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        дата = c.String(nullable: false),
                        время = c.String(nullable: false),
                        город = c.String(nullable: false),
                        погода = c.String(nullable: false),
                        UserType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserTypes", t => t.UserType_ID)
                .Index(t => t.UserType_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.история", "UserType_ID", "dbo.UserTypes");
            DropForeignKey("dbo.города", "UserType_ID", "dbo.UserTypes");
            DropIndex("dbo.история", new[] { "UserType_ID" });
            DropIndex("dbo.города", new[] { "UserType_ID" });
            DropTable("dbo.история");
            DropTable("dbo.UserTypes");
            DropTable("dbo.города");
        }
    }
}
