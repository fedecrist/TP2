using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class UsuarioLogic : BusinessLogic
    {
        public Data.Database.UsuarioAdapter UsuarioData { get; set; }
        public UsuarioLogic()
        {
            this.UsuarioData = new UsuarioAdapter();
        }

        public List<Usuario> GetAll()
        {
            return UsuarioData.GetAll();
            }
        public Business.Entities.Usuario GetOne (int Id)
        {
            return UsuarioData.GetOne(Id);
        }
        public void Delete (int Id)
        {
            UsuarioData.Delete(Id);
        }
        public void Save(Business.Entities.Usuario usuario)
        {
            UsuarioData.Save(usuario);
        }
    }
}
