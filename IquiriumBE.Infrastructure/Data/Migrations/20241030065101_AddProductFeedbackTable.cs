using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IquiriumBE.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductFeedbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    feedback = table.Column<string>(type: "text", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_feedbacks_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_feedbacks_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_feedbacks_product_id",
                table: "product_feedbacks",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_feedbacks_user_id",
                table: "product_feedbacks",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_feedbacks");
        }
    }
}
