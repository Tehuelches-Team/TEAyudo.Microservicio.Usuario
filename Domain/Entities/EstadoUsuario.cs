using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class EstadoUsuario
    {
        public int EstadoUsuarioId { get; set; }
        public string Descripcion { get; set; }
        //public int UsuarioId { get; set; }
        public Usuario Usuario { get; set;}
    }
}