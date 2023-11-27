using Application.Interface;
using Application.Mapping;
using Application.Model.DTO;
using Application.Model.Response;
using Domain.Entities;

namespace Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCommand UsuarioCommand;
        private readonly IUsuarioQuery UsuarioQuery;

        public UsuarioService(IUsuarioCommand UsuarioCommand, IUsuarioQuery UsuarioQuery)
        {
            this.UsuarioQuery = UsuarioQuery;
            this.UsuarioCommand = UsuarioCommand;
        }

        public async Task<List<UsuarioResponse>> GetAllUsuarios()
        {
            List<Usuario> ListaUsuario = await UsuarioQuery.GetAllUsuarios();
            MapUsuariosToUsuariosResponse Mapping = new MapUsuariosToUsuariosResponse();
            List<UsuarioResponse> ListaResponse = Mapping.Map(ListaUsuario);
            return ListaResponse;
        }

        public async Task<UsuarioResponse?> GetUsuarioById(int Id)
        {
            Usuario? Usuario = await UsuarioQuery.GetUsuarioById(Id);
            if (Usuario == null)
            {
                return null;
            }

            MapUsuarioToUsuarioResponse Mapping = new MapUsuarioToUsuarioResponse();
            return Mapping.Map(Usuario);
        }

        public async Task<UsuarioResponse?> PostUsuario(UsuarioDTO UsuarioRecibido, string Token)
        {
            //Crear clase validar para la fecha.
            MapUsuarioDTOToUsuario MappingUser = new MapUsuarioDTOToUsuario();
            Usuario? Usuario = MappingUser.Map(UsuarioRecibido);
            Usuario.Token = Token;
            Usuario = await UsuarioCommand.PostUsuario(Usuario);
            if (Usuario == null)
            {
                return null;
            }
            MapUsuarioToUsuarioResponse Mapping = new MapUsuarioToUsuarioResponse();
            return Mapping.Map(Usuario);
        }

        public async Task<bool> ComprobarCorreo(string Correo)
        {
            return await UsuarioQuery.GetUsuarioByEmail(Correo); //Podemos comprobar que el formato sea válido (En validation)
        }

        public async Task<UsuarioResponse?> PutUsuario(int Id, UsuarioDTO UsuarioRecibido)
        {
            Usuario Usuario = await UsuarioCommand.PutUsuario(Id, UsuarioRecibido);
            MapUsuarioToUsuarioResponse Mapping = new MapUsuarioToUsuarioResponse();
            return Mapping.Map(Usuario);
        }

        public async Task DeleteUsuario(int Id)
        {
            await UsuarioCommand.DeleteUsuario(Id);
        }

        public async Task<LogginResponse?> Loggin(string Correo,string contrasena)
        {
            Usuario usuario = await UsuarioQuery.GetUsuarioByLoggin(Correo, contrasena);
            if (usuario != null)
            {
                return new LogginResponse
                {
                    UsuarioId = usuario.UsuarioId,
                    TipoUsuario = usuario.TipoUsuario
                };
            }
            return null;
        }
    }
}
