using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class ModuloUsuario : BusinessEntity
    {
        //propiedades

        private int _IdUsuario;
        public int IdUsuario { get; set; }

        private int _IdModulo;
        public int IdModulo { get; set; }

        private bool _PermiteAlta;
        public bool PermiteAlta{get;set;}

        private bool _PermiteBaja;
        public bool PermiteBaja { get; set; }

        private bool _PermiteModificacion;
        public bool PermiteModificacion { get; set; }

        private bool _PermiteConsulta;
        public bool PermiteConsulta { get; set; }


    }
}
