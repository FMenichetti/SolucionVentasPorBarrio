using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDominio;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class DatosFavorito
    {

        public bool AgregarFavorito(int idUsuario, int idProducto, out string mensaje)
        {
            bool resultado = true;
            mensaje = string.Empty;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_AgregarFavorito");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@IdProducto", idProducto);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                datos.comando().Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                datos.ejecutarAccion();

                resultado = Convert.ToBoolean(datos.comando().Parameters["Resultado"].Value);
                mensaje = datos.comando().Parameters["Mensaje"].Value.ToString();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            datos.cerrarConexion();
            return resultado;
        }

        public bool ExisteFavorito(int idUsuario, int idProducto)
        {
            bool resultado = true;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_ExisteFavorito");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@IdProducto", idProducto);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                datos.ejecutarAccion();

                resultado = Convert.ToBoolean(datos.comando().Parameters["Resultado"].Value);
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            datos.cerrarConexion();
            return resultado;
        }

        public int CantidadEnFavoritos(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            int resultado = 0;

            try
            {
                datos.setearConsulta("Select count (*) from Favoritos where idUsuario= @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.comando().Connection = datos.conexion();
                datos.conexion().Open();
                resultado = Convert.ToInt32(datos.comando().ExecuteScalar());


            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            datos.cerrarConexion();
            return resultado;
        }

        public List<Favorito> ListarProductosFavoritos(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Favorito> lista = new List<Favorito>();

            try
            {
                datos.setearConsulta("select * from fn_ObtenerFavoritoUsuario(@IdUsuario)");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {

                    Favorito favorito = new Favorito();
                    favorito.oProducto = new Producto();
                    favorito.oProducto.IdProducto = Convert.ToInt32(datos.lector["IdProducto"]);
                    favorito.oProducto.Nombre = datos.lector["Nombre"].ToString();
                    favorito.oProducto.Descripcion = datos.lector["Descripcion"].ToString();
                    favorito.oProducto.Precio = Convert.ToDecimal(datos.lector["Precio"], new CultureInfo("es-AR"));
                    favorito.oProducto.UrlImagen = datos.lector["UrlImagen"].ToString();
                    favorito.oProducto.NombreImagen = datos.lector["NombreImagen"].ToString();
                    //favorito.oProducto.oMarca = new Marca();
                    //Favorito.oProducto.oMarca.IdMarca = Convert.ToInt32(datos.lector["IdMarcas"]);
                    //favorito.oProducto.oMarca.Descripcion = datos.lector["desMarca"].ToString();


                    lista.Add(favorito);

                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista = new List<Favorito>();

            }
            finally { datos.cerrarConexion(); }
        }

        public bool EliminarFavorito(int idCliente, int idProducto)
        {
            bool resultado = true;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_EliminarFavorito");
                datos.setearParametro("@IdUsuario", idCliente);
                datos.setearParametro("@IdProducto", idProducto);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                datos.ejecutarAccion();

                resultado = Convert.ToBoolean(datos.comando().Parameters["Resultado"].Value);
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            datos.cerrarConexion();
            return resultado;
        }

        public bool EliminarFavoritoConIdProducto( int idProducto)
        {
            bool resultado = true;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_EliminarFavoritoConIdProducto");
                datos.setearParametro("@IdProducto", idProducto);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                datos.ejecutarAccion();

                resultado = Convert.ToBoolean(datos.comando().Parameters["Resultado"].Value);
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            datos.cerrarConexion();
            return resultado;
        }
    }
}
