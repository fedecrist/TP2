using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Usuario : BusinessEntity
    {
        //propiedades

        private string _nombreusuario;
        public string NombreUsuario { get; set; }

        private string _clave;
        public string Clave { get; set; }

        private string _nombre;
        public string Nombre { get; set; }

        private string _apellido;
        public string Apellido { get; set; }

        private string _email;
        public string Email { get; set; }

        private bool _habilitado;
        public bool Habilitado { get; set; }

    }
}
