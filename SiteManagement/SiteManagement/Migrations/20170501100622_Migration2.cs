using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SiteManagement.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "MeasureUnits",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Lvs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ammount = table.Column<decimal>(nullable: false),
                    Ep = table.Column<decimal>(nullable: false),
                    Gp = table.Column<decimal>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    MeasureUnitId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PosNr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lvs_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lvs_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lvs_GroupId",
                table: "Lvs",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Lvs_MeasureUnitId",
                table: "Lvs",
                column: "MeasureUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lvs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MeasureUnits",
                newName: "id");
        }
    }
}
