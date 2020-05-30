namespace NewsFeedVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Articles", "EditedAt", c => c.DateTime());
            AlterColumn("dbo.Articles", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Articles", "DeletedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Articles", "EditedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Articles", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
