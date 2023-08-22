using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class DatosUsuario
    {
        public List<Usuario> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                datos.setearConsulta("select IdUsuario, Nombre, Apellido, Email, Pass, Restablecer, Activo, IdProvincia, IdCiudad, IdBarrio, Direccion, Celular, Tienda, IdLocal from Usuario ");
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = int.Parse(datos.lector["IdUsuario"].ToString());
                    usuario.Nombre = datos.lector["Nombre"].ToString();
                    usuario.Apellido = datos.lector["Apellido"].ToString();
                    usuario.Email = datos.lector["Email"].ToString();
                    usuario.Pass = datos.lector["Pass"].ToString();
                    usuario.Restablecer = bool.Parse(datos.lector["Restablecer"].ToString());
                    usuario.Activo = bool.Parse(datos.lector["Activo"].ToString());
                    usuario.oProvincia = new Provincia();
                    usuario.oProvincia.IdProvincia = Convert.ToInt32(datos.lector["IdProvincia"]);
                    usuario.oCiudad = new Ciudad();
                    usuario.oCiudad.IdCiudad = int.Parse(datos.lector["IdCiudad"].ToString());
                    usuario.oBarrio = new Barrio();
                    usuario.oBarrio.IdBarrio = int.Parse(datos.lector["IdBarrio"].ToString());
                    usuario.Direccion = datos.lector["Direccion"].ToString();
                    usuario.Celular = datos.lector["Celular"].ToString();
                    usuario.Tienda = datos.lector["Tienda"].ToString();
                    usuario.IdLocal = Convert.ToInt32(datos.lector["IdLocal"]);
                    lista.Add(usuario);

                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista = new List<Usuario>();

            }
            finally { datos.cerrarConexion(); }
        }

        public int RegistrarUsuarioConSP(Usuario user, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_RegistrarUsuario");
                datos.setearParametro("@Nombre", user.Nombre);
                datos.setearParametro("@Apellido", user.Apellido);
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@Pass", user.Pass);
                datos.setearParametro("@Activo", user.Activo);
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

        public bool EditarUsuarioConSp(Usuario user, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_EditarUsuario");
                datos.setearParametro("@IdUsuario", user.IdUsuario);
                datos.setearParametro("@Nombre", user.Nombre);
                datos.setearParametro("@Apellido", user.Apellido);
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@Activo", user.Activo);
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

        public bool Eliminar(int id, out string mensaje)
        {
            AccesoDatos datos = new AccesoDatos();
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                datos.setearConsulta("Delete from Usuario where IdUsuario= @Id");
                datos.setearParametro("@Id", id);
                datos.comando().Connection = datos.conexion();
                datos.conexion().Open();
                resultado = datos.comando().ExecuteNonQuery() > 0 ? true : false;


            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            datos.cerrarConexion();
            return resultado;
        }

        public bool CambiarClave(int idUsuario, string nuevaClave, out string mensaje)
        {
            AccesoDatos datos = new AccesoDatos();
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                datos.setearConsulta("update Usuario set pass = @nuevaClave, Restablecer = 0 where idUsuario = @id");
                datos.setearParametro("@Id", idUsuario);
                datos.setearParametro("@nuevaClave", nuevaClave);
                datos.comando().Connection = datos.conexion();
                datos.conexion().Open();
                resultado = datos.comando().ExecuteNonQuery() > 0 ? true : false;


            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            datos.cerrarConexion();
            return resultado;
        }

        public bool RestablecerClave(int idUsuario, string Clave, out string mensaje)
        {
            AccesoDatos datos = new AccesoDatos();
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                datos.setearConsulta("update Usuario set pass = @Clave, Restablecer = 1 where id = @id");
                datos.setearParametro("@Id", idUsuario);
                datos.setearParametro("@nuevaClave", Clave);
                datos.comando().Connection = datos.conexion();
                datos.conexion().Open();
                resultado = datos.comando().ExecuteNonQuery() > 0 ? true : false;


            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            datos.cerrarConexion();
            return resultado;
        }
        public int RegistrarUsuarioConSP_Nuevo(Usuario user, out string mensaje)
        {
            int idAutogenerado = 0;
            mensaje = string.Empty;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_RegistrarUsuarioNuevo");
                datos.setearParametro("@Nombre", user.Nombre);
                datos.setearParametro("@Apellido", user.Apellido);
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@Pass", user.Pass);
                datos.setearParametro("@Activo", user.Activo);
                datos.setearParametro("@Direccion", user.Direccion);
                datos.setearParametro("@Celular", user.Celular);
                datos.setearParametro("@Tienda", user.Tienda);
                datos.setearParametro("@IdLocal", user.IdLocal);
                datos.setearParametro("@IdProvincia", user.oProvincia.IdProvincia);
                datos.setearParametro("@IdCiudad", user.oCiudad.IdCiudad);
                datos.setearParametro("@IdBarrio", user.oBarrio.IdBarrio);
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

        public int RegistrarLocalUsuario(Usuario user)
        { //registro con nombre y devuelve IdLocal


            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_RegistrarLocal");
                datos.setearParametro("@Descripcion", user.Tienda);
                datos.comando().Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                datos.comando().Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                datos.ejecutarAccion();

                int idLocal = int.Parse(datos.comando().Parameters["Resultado"].Value.ToString());
                string mensaje = datos.comando().Parameters["Mensaje"].Value.ToString();

                return idLocal;
            }
            catch (Exception ex)
            {
                return 0;
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        //public Local ConsultaIdLocalUsuario(Usuario user)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.setearConsulta(" select IdLocal,Descripcion from Locales where Descripcion = @Descripcion ");
        //        datos.setearParametro("@Descripcion", user.Tienda);
        //        datos.ejecutarLectura();
        //        Local local= new Local();
        //        while (datos.lector.Read())
        //        {
        //            local.Descripcion = datos.lector["Descripcion"].ToString();
        //            local.IdLocal = Convert.ToInt32(datos.lector["IdLocal"]);

        //        }
        //        return local;
        //    }
        //    catch (Exception ex)
        //    {
        //            Local oLocal = new Local();
        //        return oLocal;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }

        //}
    }
}
