namespace ProjectWebMVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories(CategoryName) VALUES('Accessories')");
            Sql("INSERT INTO Categories(CategoryName) VALUES('Desktop')");
            Sql("INSERT INTO Categories(CategoryName) VALUES('Phone')");
            Sql("INSERT INTO Categories(CategoryName) VALUES('Laptop')");
            Sql("INSERT INTO Categories(CategoryName) VALUES('Monitor')");
            Sql("INSERT INTO Categories(CategoryName) VALUES('Server')");
            Sql("INSERT INTO Categories(CategoryName) VALUES('Smart Phone')");
        }
        
        public override void Down()
        {
        }
    }
}
