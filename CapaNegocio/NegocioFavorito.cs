using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaDominio;


namespace CapaNegocio
{
    public class NegocioFavorito
    {

        DatosFavorito datos = new DatosFavorito();


        public bool AgregarFavorito(int idUsuario, int idProducto, out string mensaje)
        {
            return datos.AgregarFavorito(idUsuario, idProducto,  out mensaje);
        }

        public bool ExisteFavorito(int idUsuario, int idProducto)
        {
            return datos.ExisteFavorito(idUsuario, idProducto);
        }

       

        public int CantidadEnFavorito(int idUsuario)
        {
            return datos.CantidadEnFavoritos(idUsuario);
        }

        public List<Favorito> ListarProductoFavorito(int idUsuario)
        {
            return datos.ListarProductosFavoritos(idUsuario);
        }

        public bool EliminarFavorito(int idUsuario, int idProducto)
        {
            return datos.EliminarFavorito(idUsuario, idProducto);
        }

        public bool EliminarFavoritoConIdProducto(int idProducto)
        {
            return datos.EliminarFavoritoConIdProducto(idProducto);
        }

    }
}
