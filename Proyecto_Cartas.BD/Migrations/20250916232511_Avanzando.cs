using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Cartas.BD.Migrations
{
    /// <inheritdoc />
    public partial class Avanzando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aceptado",
                table: "UsuariosPartida",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PartidaID",
                table: "UsuariosPartida",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "UsuariosPartida",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Posicion",
                table: "Rankings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Puntos",
                table: "Rankings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Rankings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Rankings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Partidas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Partidas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Ganador",
                table: "Partidas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CartaID",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Amigos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Amigos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Usuario1Id",
                table: "Amigos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Usuario2Id",
                table: "Amigos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Amigos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId2",
                table: "Amigos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPartida_PartidaID",
                table: "UsuariosPartida",
                column: "PartidaID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPartida_UsuarioID",
                table: "UsuariosPartida",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Rankings_UsuarioID",
                table: "Rankings",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_CartaID",
                table: "Inventarios",
                column: "CartaID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_UsuarioID",
                table: "Inventarios",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_Usuario1Id",
                table: "Amigos",
                column: "Usuario1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_Usuario2Id",
                table: "Amigos",
                column: "Usuario2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Amigos_Usuarios_Usuario1Id",
                table: "Amigos",
                column: "Usuario1Id",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Amigos_Usuarios_Usuario2Id",
                table: "Amigos",
                column: "Usuario2Id",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Cartas_CartaID",
                table: "Inventarios",
                column: "CartaID",
                principalTable: "Cartas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Usuarios_UsuarioID",
                table: "Inventarios",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rankings_Usuarios_UsuarioID",
                table: "Rankings",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPartida_Partidas_PartidaID",
                table: "UsuariosPartida",
                column: "PartidaID",
                principalTable: "Partidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPartida_Usuarios_UsuarioID",
                table: "UsuariosPartida",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amigos_Usuarios_Usuario1Id",
                table: "Amigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Amigos_Usuarios_Usuario2Id",
                table: "Amigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Cartas_CartaID",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Usuarios_UsuarioID",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Rankings_Usuarios_UsuarioID",
                table: "Rankings");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPartida_Partidas_PartidaID",
                table: "UsuariosPartida");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPartida_Usuarios_UsuarioID",
                table: "UsuariosPartida");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosPartida_PartidaID",
                table: "UsuariosPartida");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosPartida_UsuarioID",
                table: "UsuariosPartida");

            migrationBuilder.DropIndex(
                name: "IX_Rankings_UsuarioID",
                table: "Rankings");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_CartaID",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_UsuarioID",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Amigos_Usuario1Id",
                table: "Amigos");

            migrationBuilder.DropIndex(
                name: "IX_Amigos_Usuario2Id",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "Aceptado",
                table: "UsuariosPartida");

            migrationBuilder.DropColumn(
                name: "PartidaID",
                table: "UsuariosPartida");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "UsuariosPartida");

            migrationBuilder.DropColumn(
                name: "Posicion",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "Puntos",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "Ganador",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "CartaID",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "Usuario1Id",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "Usuario2Id",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "UsuarioId2",
                table: "Amigos");
        }
    }
}
