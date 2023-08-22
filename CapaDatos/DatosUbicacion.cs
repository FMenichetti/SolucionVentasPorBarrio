using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDominio;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DatosUbicacion
    {

        public List<Provincia> ObtenerProvincia()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Provincia> lista = new List<Provincia>();
            try
            {
                datos.setearConsulta("select * from provincias");
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Provincia provincia = new Provincia();
                    provincia.IdProvincia = Convert.ToInt32(datos.lector["IdProvincia"]);
                    provincia.Descripcion = datos.lector["Descripcion"].ToString();
                   
                    lista.Add(provincia);

                }
                return lista;
            }
            catch (Exception)
            {
                return lista = new List<Provincia>();

            }
            finally { datos.cerrarConexion(); }
        }

        public List<Ciudad> ObtenerCiudad(int idProvincia)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Ciudad> lista = new List<Ciudad>();
            try
            {
                datos.setearConsulta("select * from Ciudades where IdProvincia = iif(@IdProvincia = 0, IdProvincia, @IdProvincia)");
                datos.setearParametro("@IdProvincia", idProvincia);
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Ciudad ciudad = new Ciudad();
                    ciudad.IdCiudad = Convert.ToInt32(datos.lector["IdCiudad"]);
                    ciudad.IdProvincia = Convert.ToInt32(datos.lector["IdProvincia"]);
                    ciudad.Descripcion = datos.lector["Descripcion"].ToString();

                    lista.Add(ciudad);

                }
                return lista;
            }
            catch (Exception)
            {
                return lista = new List<Ciudad>();

            }
            finally { datos.cerrarConexion(); }
        }

        public List<Barrio> ObtenerBarrio(int idProvincia, int idCiudad)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Barrio> lista = new List<Barrio>();
            try
            {
                datos.setearConsulta("select * from Barrios where IdProvincia = iif(@IdProvincia = 0, IdProvincia, @IdProvincia) and IdCiudad = iif(@IdCiudad = 0, IdCiudad, @IdCiudad)");
                datos.setearParametro("@IdCiudad", idCiudad);
                datos.setearParametro("@IdProvincia", idProvincia);
                datos.ejecutarLectura();
                while (datos.lector.Read())
                {
                    Barrio Barrio = new Barrio();
                    Barrio.IdBarrio = Convert.ToInt32(datos.lector["IdBarrio"]);
                    Barrio.IdCiudad = Convert.ToInt32(datos.lector["IdCiudad"]);
                    Barrio.IdProvincia = Convert.ToInt32(datos.lector["IdProvincia"]);
                    Barrio.Descripcion = datos.lector["Descripcion"].ToString();

                    lista.Add(Barrio);

                }
                return lista;
            }
            catch (Exception)
            {
                return lista = new List<Barrio>();

            }
            finally { datos.cerrarConexion(); }
        }
    }
}
