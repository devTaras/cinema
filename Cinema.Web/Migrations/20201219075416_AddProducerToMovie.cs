﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Web.Migrations
{
    public partial class AddProducerToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProducerId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_ProducerId",
                table: "Movie",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Producer_ProducerId",
                table: "Movie",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Producer_ProducerId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_ProducerId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "Movie");
        }
    }
}
