using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.SqlClient;
using CapaDominio;
using System.Globalization;

namespace CapaDatos
{
    public class DatosProducto
    {


        public List<Producto> listar( )
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();
            StringBuilder str = new StringBuilder();
            str.Append(" Select p.IdProducto, p.Nombre, p.Descripcion , ");
            //str.Append(" m.IdMarcas, m.Descripcion [DescMarca], ");
            str.Append(" c.IdCategoria, c.Descripcion [descCategoria] , ");
            str.Append(" p.Precio, p.Stock, p.UrlImagen, p.NombreImagen, p.Activo ");
            str.Append(" from Producto p ");
            //str.Append(" inner join Marcas m on m.IdMarcas = p.IdMarca ");
            str.Append(" inner join Categoria c on c.IdCategoria = p.IdCategoria ");
            try
            {
                datos.setearConsulta(str.ToString());
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Producto Producto = new Producto();
                    Producto.IdProducto = int.Parse(datos.lector["IdProducto"].ToString());
                    Producto.Nombre = datos.lector["Descripcion"].ToString();
                    Producto.Descripcion = datos.lector["Descripcion"].ToString();
                    //Producto.oMarca = new Marca();
                    //Producto.oMarca.IdMarca = Convert.ToInt32(datos.lector["IdMarcas"]);
                    //Producto.oMarca.Descripcion = datos.lector["DescMarca"].ToString();
                    Producto.oCategoria = new Categoria();
                    Producto.oCategoria.IdCategoria = Convert.ToInt32(datos.lector["IdCategoria"]);
                    Producto.oCategoria.Descripcion = datos.lector["DescCategoria"].ToString();
                    Producto.Precio = Convert.ToDecimal(datos.lector["Precio"], new CultureInfo("es-AR"));
                    Producto.Stock= Convert.ToInt32(datos.lector["Stock"]);
                    Producto.UrlImagen= datos.lector["UrlImagen"].ToString();
                    Producto.NombreImagen= datos.lector["NombreImagen"].ToString();
                    Producto.Activo = Convert.ToBoolean(datos.lector["Activo"]);
                    lista.Add(Producto);

                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista = new List<Producto>();
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public List<Producto> listarConFiltros()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();
            StringBuilder str = new StringBuilder();
            str.Append(" Select p.IdProducto, p.Nombre, p.Descripcion, p.IdUsuario ,p.IdLocal, ");
            str.Append("  l.Descripcion [DescLocal], ");
            str.Append(" c.IdCategoria, c.Descripcion [descCategoria] , ");
            str.Append(" r.IdProvincia, r.Descripcion [descProvincia] , ");
            str.Append(" i.IdCiudad, i.Descripcion [descCiudad] , ");
            str.Append(" b.IdBarrio, b.Descripcion [descBarrio] , ");
            str.Append(" p.Precio, p.Stock, p.UrlImagen, p.NombreImagen, p.Activo ");
            str.Append(" from Producto p ");
            str.Append(" inner join Locales l on l.IdLocal = p.IdLocal ");
            str.Append(" inner join Categoria c on c.IdCategoria = p.IdCategoria ");
            str.Append(" inner join Provincias r on r.IdProvincia = p.IdProvincia ");
            str.Append(" inner join Ciudades i on i.IdCiudad = p.IdCiudad ");
            str.Append(" inner join Barrios b on b.IdBarrio = p.IdBarrio ");
            try
            {
                datos.setearConsulta(str.ToString());
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Producto producto = new Producto();
                    producto.IdProducto = int.Parse(datos.lector["IdProducto"].ToString());
                    producto.Nombre = datos.lector["Nombre"].ToString();
                    producto.Descripcion = datos.lector["Descripcion"].ToString();

                    producto.IdLocal = int.Parse(datos.lector["IdLocal"].ToString());
                    producto.Tienda = datos.lector["DescLocal"].ToString();

                    producto.oCategoria = new Categoria();
                    producto.oCategoria.IdCategoria = Convert.ToInt32(datos.lector["IdCategoria"]);
                    producto.oCategoria.Descripcion = datos.lector["DescCategoria"].ToString();

                    producto.oProvincia = new Provincia();
                    producto.oProvincia.IdProvincia = Convert.ToInt32(datos.lector["IdProvincia"]);
                    producto.oProvincia.Descripcion = datos.lector["DescProvincia"].ToString();

                    producto.oCiudad = new Ciudad();
                    producto.oCiudad.IdCiudad = Convert.ToInt32(datos.lector["IdCiudad"]);
                    producto.oCiudad.Descripcion = datos.lector["DescCiudad"].ToString();

                    producto.oBarrio = new Barrio();
                    producto.oBarrio.IdBarrio = Convert.ToInt32(datos.lector["IdBarrio"]);
                    producto.oBarrio.Descripcion = datos.lector["DescBarrio"].ToString();

                    producto.oUsuario = new Usuario();
                    producto.oUsuario.IdUsuario = Convert.ToInt32(datos.lector["IdUsuario"]);
                    //Producto.oUsuario.Tienda = datos.lector["Tienda"].ToString();
                    //Producto.oUsuario.Direccion = datos.lector["Direccion"].ToString();
                    //Producto.oUsuario.Celular = datos.lector["Celular"].ToString();

                    producto.Precio = Convert.ToDecimal(datos.lector["Precio"], new CultureInfo("es-AR"));
                    producto.Stock = Convert.ToInt32(datos.lector["Stock"]);
                    producto.UrlImagen = datos.lector["UrlImagen"].ToString();
                    producto.NombreImagen = datos.lector["NombreImagen"].ToString();
                    producto.Activo = Convert.ToBoolean(datos.lector["Activo"]);
                    lista.Add(producto);

                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista = new List<Producto>();
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        //p.IdProvincia, p.IdCiudad, p.IdBarrio, 
        //usado en capa usuarios
        public int RegistrarProducto_sp(Producto Producto, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("sp_RegistrarProducto");
                datos.setearParametro("@Nombre", Producto.Nombre);
                datos.setearParametro("@Descripcion", Producto.Descripcion);
                datos.setearParametro("@IdCategoria", Producto.oCategoria.IdCategoria);
                datos.setearParametro("@IdMarca", Producto.oMarca.IdMarca);
                datos.setearParametro("@Precio", Producto.Precio);
                datos.setearParametro("@Stock", Producto.Stock);
                datos.setearParametro("@Activo", Producto.Activo);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                datos.comando().Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                datos.ejecutarAccion();

                idAutogenerado = int.Parse(datos.comando().Parameters["Resultado"].Value.ToString());
                mensaje = datos.comando().Parameters["Mensaje"].Value.ToString();
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                mensaje = ex.Message;

            }
            datos.cerrarConexion();
            return idAutogenerado;
        }

        public bool EditarProducto_Sp(Producto Producto, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_EditarProducto");
                datos.setearParametro("@IdProducto", Producto.IdProducto);
                datos.setearParametro("@Nombre", Producto.Nombre);
                datos.setearParametro("@Descripcion", Producto.Descripcion);
                datos.setearParametro("@IdCategoria", Producto.oCategoria.IdCategoria);
                //datos.setearParametro("@IdMarca", Producto.oMarca.IdMarca);
                datos.setearParametro("@Precio", Producto.Precio);
                datos.setearParametro("@Stock", Producto.Stock);
                datos.setearParametro("@Activo",Producto.Activo);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        public bool EliminarProducto_sp(int idProducto, out string mensaje)
        {

            bool resultado = false;
            mensaje = string.Empty;

            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearProcedimiento("sp_EliminarProducto");
                datos.setearParametro("@IdProducto", idProducto);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                datos.comando().Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                datos.comando().Connection = datos.conexion();
                datos.conexion().Open();
                resultado = datos.comando().ExecuteNonQuery() > 0 ? true : false;

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

        public bool GuardarDatosImagen_sp(Producto Producto, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update Producto set UrlImagen = @UrlImagen, NombreImagen = @NombreImagen where IdProducto = @IdProducto");
                datos.setearParametro("@UrlImagen", Producto.UrlImagen);
                datos.setearParametro("@NombreImagen", Producto.NombreImagen);
                datos.setearParametro("@IdProducto", Producto.IdProducto);
                datos.comando().Connection = datos.conexion();
                datos.conexion().Open();
                if (datos.comando().ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                else 
                {
                    resultado = false;
                    mensaje = "No se pudo actualizar imagen";
                }
 
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            datos.cerrarConexion();
            return resultado;
        }

        public int RegistrarProducto_Tienda(Producto Producto, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("nuevo_RegistrarProducto");
                datos.setearParametro("@Nombre", Producto.Nombre);
                datos.setearParametro("@Descripcion", Producto.Descripcion);
                datos.setearParametro("@IdUsuario", Producto.oUsuario.IdUsuario);
                datos.setearParametro("@IdCategoria", Producto.oCategoria.IdCategoria);
                //datos.setearParametro("@IdMarca", Producto.oMarca.IdMarca);
                datos.setearParametro("@IdProvincia", Producto.oProvincia.IdProvincia);
                datos.setearParametro("@IdCiudad", Producto.oCiudad.IdCiudad);
                datos.setearParametro("@IdBarrio", Producto.oBarrio.IdBarrio);
                datos.setearParametro("@IdLocal", Producto.IdLocal);
                datos.setearParametro("@Precio", Producto.Precio);
                datos.setearParametro("@Stock", Producto.Stock);
                datos.setearParametro("@Activo", Producto.Activo);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                datos.comando().Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                datos.ejecutarAccion();

                idAutogenerado = int.Parse(datos.comando().Parameters["Resultado"].Value.ToString());
                mensaje = datos.comando().Parameters["Mensaje"].Value.ToString();
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                mensaje = ex.Message;

            }
            datos.cerrarConexion();
            return idAutogenerado;
        }
        public List<Producto> ListarProductosTiendaUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Producto> lista = new List<Producto>();
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT DISTINCT p.IdProducto, p.Nombre, p.Descripcion , ");
            //str.Append(" m.IdMarcas, m.Descripcion [DescMarca], ");
            str.Append(" c.IdCategoria, c.Descripcion [descCategoria] , ");
            str.Append(" p.Precio, p.Stock, p.UrlImagen, p.NombreImagen, p.Activo ");
            str.Append(" from Producto p ");
            //str.Append(" inner join Marcas m on m.IdMarcas = p.IdMarca ");
            str.Append(" inner join Categoria c on c.IdCategoria = p.IdCategoria ");
            str.Append("  where   p.IdUsuario = @IdUsuario ");
            try
            {
                datos.setearConsulta(str.ToString());
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Producto Producto = new Producto();
                    Producto.IdProducto = int.Parse(datos.lector["IdProducto"].ToString());
                    Producto.Nombre = datos.lector["Nombre"].ToString();
                    Producto.Descripcion = datos.lector["Descripcion"].ToString();
                    Producto.oMarca = new Marca();
                    //Producto.oMarca.IdMarca = Convert.ToInt32(datos.lector["IdMarcas"]);
                    //Producto.oMarca.Descripcion = datos.lector["DescMarca"].ToString();
                    Producto.oCategoria = new Categoria();
                    Producto.oCategoria.IdCategoria = Convert.ToInt32(datos.lector["IdCategoria"]);
                    Producto.oCategoria.Descripcion = datos.lector["DescCategoria"].ToString();
                    Producto.Precio = Convert.ToDecimal(datos.lector["Precio"], new CultureInfo("es-AR"));
                    Producto.Stock = Convert.ToInt32(datos.lector["Stock"]);
                    Producto.UrlImagen = datos.lector["UrlImagen"].ToString();
                    Producto.NombreImagen = datos.lector["NombreImagen"].ToString();
                    Producto.Activo = Convert.ToBoolean(datos.lector["Activo"]);
                    lista.Add(Producto);

                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista = new List<Producto>();
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
