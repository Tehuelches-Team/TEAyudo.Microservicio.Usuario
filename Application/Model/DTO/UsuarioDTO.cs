﻿namespace Application.Model.DTO
{
    public class UsuarioDTO
    {
        public int CUIL { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string FotoPerfil { get; set; }
        public string Domicilio { get; set; }
        public string FechaNacimiento { get; set; }
        public int TipoUsuario { get; set; }
    }
}
