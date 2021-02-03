// Copyright (c) Binshop and Contributors. All rights reserved.
// Licensed under the MIT License

using Microsoft.EntityFrameworkCore.Migrations;

namespace Binshop.Data.Catalog.Migrations
{
    public partial class ExternalTransactionId4PaymentTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalTransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalTransactionId",
                table: "Transactions");
        }
    }
}
