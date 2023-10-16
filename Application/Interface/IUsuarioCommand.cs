﻿using Application.Model.DTO;
using Application.Model.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUsuarioCommand
    {
        Task<Usuario?> PostUsuario(Usuario Usuario);
        Task<Usuario> PutUsuario(int Id , UsuarioDTO Usuario);
    }
}
