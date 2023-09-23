
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "EstadoUsuarios",
                columns: table => new
                {
                    EstadoUsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoUsuarios", x => x.EstadoUsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoUsuarioId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_EstadoUsuarios_EstadoUsuarioId",
                        column: x => x.EstadoUsuarioId,
                        principalTable: "EstadoUsuarios",
                        principalColumn: "EstadoUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_EstadoUsuarios_EstadoUsuarioId",
                table: "EstadoUsuarios",
                column: "EstadoUsuarioId",
                unique: true);

            migrationBuilder.InsertData(
                table: "EstadoUsuarios", // Nombre de la tabla
                columns: new[] { "Descripcion" }, // Columnas en las que deseas insertar datos
                values: new object[] { "Validado" });

            migrationBuilder.InsertData(
                table: "EstadoUsuarios", // Nombre de la tabla
                columns: new[] { "Descripcion" }, // Columnas en las que deseas insertar datos
                values: new object[] { "Pendinte" });

            migrationBuilder.InsertData(
                table: "EstadoUsuarios", // Nombre de la tabla
                columns: new[] { "Descripcion" }, // Columnas en las que deseas insertar datos
                values: new object[] { "Bloqueado" });
            migrationBuilder.InsertData(
               table: "Usuarios",
               columns: new[] { "Nombre", "Apellido", "CorreoElectronico", "Contrasena", "FotoPerfil", "Domicilio", "FechaNacimiento", "EstadoUsuarioId", "Token" },
               values: new object[] {  "Ariel",
                                        "Ortiz",
                                        "aortiz@yopmail.com",
                                        "1q2w3e4r" ,
                                        "/user/img/arielortiz.jpg" ,
                                        "Montevideo 600" ,
                                        "1980/10/01",
                                        "2",
                                        "EJzGKfVlNrAqwSt9PoL8Y3D4R5I6S7A1" });

            migrationBuilder.InsertData(
               table: "Usuarios",
               columns: new[] { "Nombre", "Apellido", "CorreoElectronico", "Contrasena", "FotoPerfil", "Domicilio", "FechaNacimiento", "EstadoUsuarioId", "Token" },
               values: new object[] {  "Pablo",
                                        "Morel",
                                        "pmorel@yopmail.com",
                                        "1q2w3e4r" ,
                                        "/user/img/pablomorel.jpg" ,
                                        "Reconquista 2500" ,
                                        "1980/10/01",
                                        "2",
                                        "b2oFxBTAvHcZ1sp3iQXOqI6rP9eN7D5w" });
            migrationBuilder.InsertData(
               table: "Usuarios",
               columns: new[] { "Nombre", "Apellido", "CorreoElectronico", "Contrasena", "FotoPerfil", "Domicilio", "FechaNacimiento", "EstadoUsuarioId", "Token" },
               values: new object[] {  "Marcelo",
                                        "Zona",
                                        "mzona@yopmail.com",
                                        "1q2w3e4r" ,
                                        "/user/img/marcelozona.jpg" ,
                                        "Paseo Colon 2500" ,
                                        "1980/10/01",
                                        "2",
                                        "G1jRkL2wPvOuI6NqQs9yE3TcXfA4bD5o" });
        }




        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.DropTable(
                name: "EstadoUsuarios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
