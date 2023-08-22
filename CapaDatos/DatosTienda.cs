using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaDominio;

namespace CapaDatos
{
     public class DatosTienda
    {

        public List<Usuario> ListarTiendaPorUbicacion(int idProvincia, int idCiudad, int idBarrio)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();
           

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" select u.tienda , l.idLocal, l.Descripcion ");
            sb.AppendLine(" from usuario u ");
            sb.AppendLine(" inner join locales l  on l.idLocal = u.idlocal ");
            sb.AppendLine(" where ");
            sb.AppendLine(" idProvincia = iif(@idProvincia = 0, idProvincia, @idProvincia) ");
            sb.AppendLine("  and  IdCiudad = iif(@idCiudad = 0, IdCiudad, @idCiudad)  ");
            sb.AppendLine(" and IdBarrio = iif(@idBarrio = 0, idBarrio, @idBarrio)  ");

            try
            {
                datos.setearConsulta(sb.ToString());
                datos.setearParametro("@idProvincia", idProvincia);
                datos.setearParametro("@idCiudad", idCiudad);
                datos.setearParametro("@idBarrio", idBarrio);
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Usuario usuario = new Usuario();
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

    }
}
