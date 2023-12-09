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
        public string GenerateToken(string email, string password);
    }
}
