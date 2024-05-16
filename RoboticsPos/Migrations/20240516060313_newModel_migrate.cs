using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoboticsPos.Migrations
{
    /// <inheritdoc />
    public partial class newModel_migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Clients_ClientId",
                table: "Shops");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "Shops",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaySum",
                table: "Shops",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DebtSum = table.Column<decimal>(type: "TEXT", nullable: false),
                    DebtStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<long>(type: "INTEGER", nullable: false),
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Debts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sum = table.Column<decimal>(type: "TEXT", nullable: false),
                    PayedSum = table.Column<decimal>(type: "TEXT", nullable: false),
                    RemainingSum = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    DebtId = table.Column<long>(type: "INTEGER", nullable: true),
                    shopId = table.Column<long>(type: "INTEGER", nullable: true),
                    DebtPaymentId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Debts_DebtId",
                        column: x => x.DebtId,
                        principalTable: "Debts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Shops_shopId",
                        column: x => x.shopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DebtPayment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DebtId = table.Column<long>(type: "INTEGER", nullable: false),
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebtPayment_Debts_DebtId",
                        column: x => x.DebtId,
                        principalTable: "Debts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DebtPayment_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DebtPayment_DebtId",
                table: "DebtPayment",
                column: "DebtId");

            migrationBuilder.CreateIndex(
                name: "IX_DebtPayment_PaymentId",
                table: "DebtPayment",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Debts_ClientId",
                table: "Debts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DebtId",
                table: "Payments",
                column: "DebtId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_shopId",
                table: "Payments",
                column: "shopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Clients_ClientId",
                table: "Shops",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Clients_ClientId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "DebtPayment");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.DropColumn(
                name: "TotalPaySum",
                table: "Shops");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "Shops",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Clients_ClientId",
                table: "Shops",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
