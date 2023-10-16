using Application.Interface;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class UsuarioCommand : IUsuarioCommand
    {
        private readonly TEAyudoContext Context;
        public UsuarioCommand(TEAyudoContext Context)
        {
            this.Context = Context;
        }
    }
}
