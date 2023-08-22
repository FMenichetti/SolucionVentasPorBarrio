using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaDominio;


namespace CapaNegocio
{
    public class NegocioTienda
    {
        DatosTienda datos = new DatosTienda();
        public List<Usuario> ListarTiendaPorUbicacion(int idProvincia, int idCiudad, int idBarrio)
        {
            return datos.ListarTiendaPorUbicacion(idProvincia, idCiudad, idBarrio);
        }

    }
}
