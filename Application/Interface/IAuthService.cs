using Application.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAuthService
    {
        public Task<string> VerificarToken(string token);
        public string GenerateToken(string nombre, string apellido, string foto, string correo);
    }
}
