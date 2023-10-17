using Application.Model.Response;
using Domain.Entities;

namespace Application.Mapping
{
    public class MapUsuariosToUsuariosResponse
    {
        public List<UsuarioResponse> Map(List<Usuario> ListaUsuario)
        {
            List<UsuarioResponse> ListaResponse = new List<UsuarioResponse>();
            UsuarioResponse UsuarioResponse;
            MapUsuarioToUsuarioResponse Mapping = new MapUsuarioToUsuarioResponse();
            foreach (Usuario Usuario in ListaUsuario)
            {
                UsuarioResponse = Mapping.Map(Usuario);
                ListaResponse.Add(UsuarioResponse);
            }
            return ListaResponse;
        }
    }
}
