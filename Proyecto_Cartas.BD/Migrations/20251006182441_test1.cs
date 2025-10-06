using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Cartas.BD.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cartas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCarta = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    TipoCarta = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    NivelCarta = table.Column<int>(type: "int", nullable: false),
                    Ataque = table.Column<int>(type: "int", nullable: false),
                    Vida = table.Column<int>(type: "int", nullable: false),
                    Velocidad = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ganador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sobres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSobre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CantidadCartas = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartasSobre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartaID = table.Column<int>(type: "int", nullable: false),
                    SobreID = table.Column<int>(type: "int", nullable: false),
                    ProbabilidadCartaSobre = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartasSobre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartasSobre_Cartas_CartaID",
                        column: x => x.CartaID,
                        principalTable: "Cartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartasSobre_Sobres_SobreID",
                        column: x => x.SobreID,
                        principalTable: "Sobres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId1 = table.Column<int>(type: "int", nullable: false),
                    Usuario1Id = table.Column<int>(type: "int", nullable: true),
                    UsuarioId2 = table.Column<int>(type: "int", nullable: false),
                    Usuario2Id = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amigos_Usuarios_Usuario1Id",
                        column: x => x.Usuario1Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Amigos_Usuarios_Usuario2Id",
                        column: x => x.Usuario2Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Billeteras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    cantidadMonedas = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billeteras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billeteras_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComprasSobre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    SobreID = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprasSobre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprasSobre_Sobres_SobreID",
                        column: x => x.SobreID,
                        principalTable: "Sobres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprasSobre_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    VolumenMusica = table.Column<int>(type: "int", nullable: false),
                    VolumenSFX = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesUsuario_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    CartaID = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_Cartas_CartaID",
                        column: x => x.CartaID,
                        principalTable: "Cartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventarios_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilesUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Experiencia = table.Column<int>(type: "int", nullable: false),
                    PartidasJugadas = table.Column<int>(type: "int", nullable: false),
                    PartidasGanadas = table.Column<int>(type: "int", nullable: false),
                    UltimoLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilesUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilesUsuario_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rankings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Puntos = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posicion = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rankings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rankings_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartasApertura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraSobreID = table.Column<int>(type: "int", nullable: false),
                    CartaSobreID = table.Column<int>(type: "int", nullable: false),
                    CantidadCartasObtenidas = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartasApertura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartasApertura_CartasSobre_CartaSobreID",
                        column: x => x.CartaSobreID,
                        principalTable: "CartasSobre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartasApertura_ComprasSobre_CompraSobreID",
                        column: x => x.CompraSobreID,
                        principalTable: "ComprasSobre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPartida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilUsuarioID = table.Column<int>(type: "int", nullable: false),
                    PartidaID = table.Column<int>(type: "int", nullable: false),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false),
                    CartasPerdidas = table.Column<int>(type: "int", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPartida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosPartida_Partidas_PartidaID",
                        column: x => x.PartidaID,
                        principalTable: "Partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosPartida_PerfilesUsuario_PerfilUsuarioID",
                        column: x => x.PerfilUsuarioID,
                        principalTable: "PerfilesUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadosCarta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioPartidaID = table.Column<int>(type: "int", nullable: false),
                    InventarioID = table.Column<int>(type: "int", nullable: false),
                    Ataque = table.Column<int>(type: "int", nullable: false),
                    Vida = table.Column<int>(type: "int", nullable: false),
                    Velocidad = table.Column<int>(type: "int", nullable: false),
                    Posicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCarta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadosCarta_Inventarios_InventarioID",
                        column: x => x.InventarioID,
                        principalTable: "Inventarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadosCarta_UsuariosPartida_UsuarioPartidaID",
                        column: x => x.UsuarioPartidaID,
                        principalTable: "UsuariosPartida",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioPartidaID = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Fase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_UsuariosPartida_UsuarioPartidaID",
                        column: x => x.UsuarioPartidaID,
                        principalTable: "UsuariosPartida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoID = table.Column<int>(type: "int", nullable: false),
                    EstadoCartaID = table.Column<int>(type: "int", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_EstadosCarta_EstadoCartaID",
                        column: x => x.EstadoCartaID,
                        principalTable: "EstadosCarta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_Turnos_TurnoID",
                        column: x => x.TurnoID,
                        principalTable: "Turnos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_Usuario1Id",
                table: "Amigos",
                column: "Usuario1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_Usuario2Id",
                table: "Amigos",
                column: "Usuario2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Billeteras_UsuarioID",
                table: "Billeteras",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_CartasApertura_CartaSobreID",
                table: "CartasApertura",
                column: "CartaSobreID");

            migrationBuilder.CreateIndex(
                name: "IX_CartasApertura_CompraSobreID",
                table: "CartasApertura",
                column: "CompraSobreID");

            migrationBuilder.CreateIndex(
                name: "IX_CartasSobre_CartaID",
                table: "CartasSobre",
                column: "CartaID");

            migrationBuilder.CreateIndex(
                name: "IX_CartasSobre_SobreID",
                table: "CartasSobre",
                column: "SobreID");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasSobre_SobreID",
                table: "ComprasSobre",
                column: "SobreID");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasSobre_UsuarioID",
                table: "ComprasSobre",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesUsuario_UsuarioID",
                table: "ConfiguracionesUsuario",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosCarta_InventarioID",
                table: "EstadosCarta",
                column: "InventarioID");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosCarta_UsuarioPartidaID",
                table: "EstadosCarta",
                column: "UsuarioPartidaID");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_EstadoCartaID",
                table: "Eventos",
                column: "EstadoCartaID");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_TurnoID",
                table: "Eventos",
                column: "TurnoID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_CartaID",
                table: "Inventarios",
                column: "CartaID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_UsuarioID",
                table: "Inventarios",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilesUsuario_UsuarioID",
                table: "PerfilesUsuario",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "PerfilesUsuario_Nombre_UQ",
                table: "PerfilesUsuario",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rankings_UsuarioID",
                table: "Rankings",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_UsuarioPartidaID",
                table: "Turnos",
                column: "UsuarioPartidaID");

            migrationBuilder.CreateIndex(
                name: "Usuarios_Email_UQ",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPartida_PartidaID",
                table: "UsuariosPartida",
                column: "PartidaID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPartida_PerfilUsuarioID",
                table: "UsuariosPartida",
                column: "PerfilUsuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amigos");

            migrationBuilder.DropTable(
                name: "Billeteras");

            migrationBuilder.DropTable(
                name: "CartasApertura");

            migrationBuilder.DropTable(
                name: "ConfiguracionesUsuario");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Rankings");

            migrationBuilder.DropTable(
                name: "CartasSobre");

            migrationBuilder.DropTable(
                name: "ComprasSobre");

            migrationBuilder.DropTable(
                name: "EstadosCarta");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Sobres");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "UsuariosPartida");

            migrationBuilder.DropTable(
                name: "Cartas");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "PerfilesUsuario");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
