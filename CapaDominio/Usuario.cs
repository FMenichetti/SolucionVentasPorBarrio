using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string ConfirmarPass { get; set; }
        public bool Restablecer { get; set; }
        public bool Activo { get; set; }
        public bool Administrador { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public Provincia oProvincia { get; set; }
        public Ciudad oCiudad { get; set; }
        public Barrio oBarrio { get; set; }
        public string Tienda { get; set; }
        public int IdLocal { get; set; }

    }
}
