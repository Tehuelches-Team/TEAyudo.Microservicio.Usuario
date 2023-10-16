using Application.Interface;
using Application.Mapping;
using Application.Model.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCommand UsuarioCommand;
        private readonly IUsuarioQuery UsuarioQuery;

        public UsuarioService (IUsuarioCommand UsuarioCommand, IUsuarioQuery UsuarioQuery) 
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
    }
}
