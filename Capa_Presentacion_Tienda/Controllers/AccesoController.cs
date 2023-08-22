using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaDominio;
using CapaNegocio;

//Voy a cambiar todos los cliente por usuario, y todos los metodos de clientes por los mismos que usa la capa de usuario

namespace Capa_Presentacion_Tienda.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }


        public ActionResult Restablecer()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();

        }
        //[HttpPost]
        //public ActionResult Registrar(Usuario usuario)
        //{
        //    int resultado;
        //    string mensaje = string.Empty;

        //    ViewData["Nombre"] = string.IsNullOrEmpty(usuario.Nombre);
        //    ViewData["Apellido"] = string.IsNullOrEmpty(usuario.Apellido);
        //    ViewData["Email"] = string.IsNullOrEmpty(usuario.Email);

        //    if (usuario.Pass != usuario.ConfirmarPass)
        //    {
        //        ViewBag.Error = "Las contraseñas no coinciden";
        //        return View();
        //    }

        //    resultado = new NegocioUsuarios().RegistrarUsuarioConSP(usuario, out mensaje);

        //    if (resultado > 0)
        //    {
        //        ViewBag.Error = null;
        //        return RedirectToAction("Index", "Acceso");
        //    }
        //    else
        //    {
        //        ViewBag.Error = mensaje;
        //        return View();
        //    }
        //}
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Session.Clear();
            Usuario usuario;
            usuario = new NegocioUsuarios().Listar().Where(x => x.Email == correo && x.Pass == NegocioRecursos.ConvertirSHA256(clave)).FirstOrDefault();

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
            return View();

            }
            else
            {
                if (usuario.Restablecer)
                {
                    TempData["idCliente"] = usuario.IdUsuario;
                    return RedirectToAction("CambiarClave", "Acceso");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(usuario.Email, false);
                    Session["cliente"] = usuario;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }

        }

        [HttpPost]
        public ActionResult Restablecer(string correo)
        {
            Usuario usuario = new Usuario();
            usuario = new NegocioUsuarios().Listar().Where(x => x.Email == correo).FirstOrDefault();

            if (usuario == null)
            {
                ViewBag.Error = "No se encontro el Cliente relacionado a ese Correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new NegocioCliente().RestablecerClave(usuario.IdUsuario, correo, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();

            }
        }
        [HttpPost]
        public ActionResult CambiarClave(string idCliente, string claveActual, string claveNueva, string confirmarClave)
        {
            Usuario usuario = new Usuario();
            usuario = new NegocioUsuarios().Listar().Where(i => i.IdUsuario == int.Parse(idCliente)).FirstOrDefault();
            if (usuario.Pass != NegocioRecursos.ConvertirSHA256(claveActual))
            {
                TempData["idCliente"] = idCliente;
                ViewData["clave"] = "";
                ViewBag.Error = "La contraseña actual no coincide";
                return View();
            }
            else if (claveNueva != confirmarClave)
            {
                ViewData["clave"] = claveActual;
                TempData["idCliente"] = idCliente;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["clave"] = "";

            claveNueva = NegocioRecursos.ConvertirSHA256(claveNueva);
            string mensaje = string.Empty;
            bool respuesta = new NegocioCliente().CambiarClave(int.Parse(idCliente), claveNueva, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index","Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                TempData["idCliente"] = idCliente;
                return View();
            }


        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }

        [HttpPost]
        public ActionResult Registrar_nuevo(Usuario usuario, int provinciaId, int ciudadId, int barrioId)
        {
            int resultado;
            string mensaje = string.Empty;
            usuario.oProvincia = new Provincia();
            usuario.oCiudad = new Ciudad();
            usuario.oBarrio = new Barrio();
            


            ViewData["Nombre"] = string.IsNullOrEmpty(usuario.Nombre);
            ViewData["Apellido"] = string.IsNullOrEmpty(usuario.Apellido);
            ViewData["Email"] = string.IsNullOrEmpty(usuario.Email);
            ViewData["Direccion"] = string.IsNullOrEmpty(usuario.Direccion);
            ViewData["Celular"] = string.IsNullOrEmpty(usuario.Celular);
            ViewData["Tienda"] = string.IsNullOrEmpty(usuario.Tienda);
            usuario.oProvincia.IdProvincia = provinciaId;
            ViewData["Provincia"] = usuario.oProvincia.IdProvincia;
            usuario.oCiudad.IdCiudad= ciudadId;
            ViewData["Ciudad"] = string.IsNullOrEmpty(usuario.oCiudad.Descripcion);
            usuario.oBarrio.IdBarrio = barrioId;
            ViewData["Barrio"] = string.IsNullOrEmpty(usuario.oBarrio.Descripcion);

          
            resultado = new NegocioUsuarios().RegistrarUsuarioConSP_Nuevo(usuario, out mensaje);

            if (resultado > 0)
            {
                bool registro = true;
                Session["registro"] = registro;
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return RedirectToAction("Registrar", "Acceso");
            }
        }

        //UBICACION
        [HttpPost]
        public JsonResult ObtenerProvincia()
        {
            List<Provincia> lista = new List<Provincia>();

            lista = new NegocioUbicacion().ObtenerProvincia();

            return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerCiudad(int idProvincia)
        {
            List<Ciudad> lista = new List<Ciudad>();

            lista = new NegocioUbicacion().ObtenerCiudad(idProvincia);

            return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerBarrio(int idProvincia, int idCiudad)
        {
            List<Barrio> lista = new List<Barrio>();

            lista = new NegocioUbicacion().ObtenerBarrio(idProvincia, idCiudad);

            return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        }

    }
}