using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addviewMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
create view vInvoiceMaster as
SELECT        dbo.InvoiceMasters.Id, dbo.InvoiceMasters.CustomerId, dbo.InvoiceMasters.StoreId, dbo.InvoiceMasters.DateInvoice, dbo.InvoiceMasters.Notes, dbo.InvoiceMasters.SumPrice, dbo.InvoiceMasters.DiscountPrecent, 
                         dbo.InvoiceMasters.DiscountVal, dbo.InvoiceMasters.Net, dbo.Customers.Name AS CustomersName, dbo.Stores.Name AS StoresName
FROM            dbo.Customers INNER JOIN
                         dbo.InvoiceMasters ON dbo.Customers.Id = dbo.InvoiceMasters.CustomerId INNER JOIN
                         dbo.Stores ON dbo.InvoiceMasters.StoreId = dbo.Stores.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
drop view vInvoiceMaster;
");

        }
    }
}
