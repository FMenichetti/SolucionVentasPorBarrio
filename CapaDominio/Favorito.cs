using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Favorito
    {

        public int IdFavorito { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public Producto oProducto { get; set; }//revisar lo agregue 

    }
}
