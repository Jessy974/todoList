namespace todoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTodo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Todoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        Done = c.Boolean(nullable: false),
                        DeadLineDate = c.DateTime(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todoes", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Todoes", new[] { "CategoryID" });
            DropTable("dbo.Todoes");
        }
    }
}
