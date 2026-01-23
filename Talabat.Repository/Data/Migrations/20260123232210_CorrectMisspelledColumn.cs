using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrectMisspelledColumn : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			// Rename the column in OrderItems table
			migrationBuilder.RenameColumn(
				name: "Qauntity",     
				table: "OrderItems",   
				newName: "Quantity");  
		}


		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Quantity",
				table: "OrderItems",
				newName: "Qauntity");
		}
	}
}
