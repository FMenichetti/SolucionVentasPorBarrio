using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDominio;
using CapaNegocio;
using System.IO;

//

namespace Capa_Presentacion_Tienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Producto()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetalleProducto(int idProducto = 0)//modificando listar
        {
            Producto producto = new Producto();
            bool conversion;

            producto.oUsuario = new Usuario();
            producto = new NegocioProducto().ListarConFiltros().Where(p => p.IdProducto == idProducto).FirstOrDefault();

            producto.oUsuario = new NegocioUsuarios().Listar().Where(u => u.IdUsuario == producto.oUsuario.IdUsuario).FirstOrDefault();

            if (producto != null)
            {
                producto.base64 = NegocioRecursos.ConvertirBase64(Path.Combine(producto.UrlImagen, producto.NombreImagen), out conversion);
                producto.extension = Path.GetExtension(producto.NombreImagen);
            }

            return View(producto);
        }

        [HttpPost]
        public JsonResult ListarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new NegocioCategorias().Listar();

            return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        }
        //Listar marcas por categoria
        //[HttpPost]
        //public JsonResult ListarMarcaPorCategoria(int idCategoria)
        //{
        //    List<Marca> lista = new List<Marca>();
        //    lista = new NegocioMarcas().ListarMarcaPorCategoria(idCategoria);

        //    return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult ListarTiendaPorUbicacion(int idProvincia, int idCiudad, int idBarrio)
        {
            List<Usuario> lista = new List<Usuario>();
            lista = new NegocioTienda().ListarTiendaPorUbicacion(idProvincia, idCiudad, idBarrio);

            return Json(new { lista = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProductos(int idCategoria, int idLocales, int idProvincia, int idCiudad, int idBarrio)
        {
            List<Producto> lista = new List<Producto>();
            bool conversion;

            lista = new NegocioProducto().ListarConFiltros().Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarca = p.oMarca,
                oCategoria = p.oCategoria,
                oProvincia = p.oProvincia,
                oCiudad = p.oCiudad,
                oBarrio = p.oBarrio,
                IdLocal = p.IdLocal,
                Tienda = p.Tienda,
                Precio = p.Precio,
                Stock = p.Stock,
                UrlImagen = p.UrlImagen,
                base64 = NegocioRecursos.ConvertirBase64(Path.Combine(p.UrlImagen, p.NombreImagen), out conversion),
                extension = Path.GetExtension(p.NombreImagen),
                Activo = p.Activo
            }).Where(p =>
                p.oProvincia.IdProvincia == (idProvincia == 0 ? p.oProvincia.IdProvincia : idProvincia) &&
                p.oCiudad.IdCiudad == (idCiudad == 0 ? p.oCiudad.IdCiudad : idCiudad) &&
                p.oBarrio.IdBarrio == (idBarrio == 0 ? p.oBarrio.IdBarrio : idBarrio) &&
                p.IdLocal == (idLocales == 0 ? p.IdLocal : idLocales) &&
                p.oCategoria.IdCategoria == (idCategoria == 0 ? p.oCategoria.IdCategoria : idCategoria) &&
                p.Stock > 0 &&
                p.Activo == true
                ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;
        }


        [HttpGet]
        public JsonResult CantidadEnCarrito()
        {
            int idCliente = ((Cliente)Session["cliente"]).IdCliente;
            int cantidad = new NegocioCarrito().CantidadEnCarrito(idCliente);

            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }
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

        //public ActionResult Carrito()
        //{
        //    return View();
        //}

        //Operaciones de Carrito
        //[HttpPost]
        //public JsonResult ListarProductosCarrito()
        //{
        //    int idCliente = ((Cliente)Session["cliente"]).IdCliente;
        //    List<Carrito> lista = new List<Carrito>();

        //    bool conversion;

        //    lista = new NegocioCarrito().ListarProducto(idCliente).Select(oc => new Carrito()
        //    {

        //        oProducto = new Producto()
        //        {

        //            IdProducto = oc.oProducto.IdProducto,
        //            Nombre = oc.oProducto.Nombre,
        //            oMarca = oc.oProducto.oMarca,
        //            Precio = oc.oProducto.Precio,
        //            UrlImagen = oc.oProducto.UrlImagen,
        //            base64 = NegocioRecursos.ConvertirBase64(Path.Combine(oc.oProducto.UrlImagen, oc.oProducto.NombreImagen), out conversion),
        //            extension = Path.GetExtension(oc.oProducto.NombreImagen)
        //        },
        //        Cantidad = oc.Cantidad


        //    }).ToList();

        //    return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

        //}
        //[HttpPost]
        //public JsonResult OperacionCarrito(int idProducto, bool sumar)
        //{
        //    int idCliente = ((Cliente)Session["cliente"]).IdCliente;

        //    bool respuesta = false;
        //    string mensaje = string.Empty;

        //    respuesta = new NegocioCarrito().OperacionCarrito(idCliente, idProducto, true, out mensaje);
           
        //    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult EliminarCarrito(int idProducto)
        //{
        //    int idCliente = ((Cliente)Session["cliente"]).IdCliente;

        //    bool respuesta = false;
        //    string mensaje = string.Empty;

        //    respuesta = new NegocioCarrito().EliminarCarrito(idCliente, idProducto);

        //    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult AgregarCarrito(int idProducto)
        //{
        //    int idCliente = ((Cliente)Session["cliente"]).IdCliente;

        //    bool existe = new NegocioCarrito().ExisteCarrito(idCliente, idProducto);

        //    bool respuesta = false;
        //    string mensaje = string.Empty;

        //    if (existe)
        //    {
        //        mensaje = "El producto ya existe en el carrito";
        //    }
        //    else
        //    {
        //        respuesta = new NegocioCarrito().OperacionCarrito(idCliente, idProducto, true, out mensaje);
        //    }
        //    return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        //}
    }
}