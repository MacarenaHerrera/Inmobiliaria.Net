
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaBase.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly RepositorioPropietario repositorioPropietario;
        private readonly IConfiguration config;

        public PropietarioController(IConfiguration config)
        {
            repositorioPropietario = new RepositorioPropietario();
            this.config = config;
        }
        // GET: PropietarioController
        public ActionResult Index()
        {
            var lista = repositorioPropietario.Obtener();
            ViewData[nameof(Propietario)] = lista;
            return View();
        }

        // GET: PropietarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PropietarioController/Create
        public ActionResult Crear()
        {

            return View();
        }

        // POST: PropietarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Propietario propietario)
        {
            try
            {
                repositorioPropietario.Alta(propietario);
                TempData["Id"] = propietario.Id;
                //return RedirectToAction(nameof(Index));            
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: PropietarioController/Edit/5
        public ActionResult Editar(int id)
        {
            var prop = repositorioPropietario.obtenerPropietario(id);
            return View(prop);
        }

        // POST: PropietarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                Propietario prop = repositorioPropietario.obtenerPropietario(id);
                prop.Nombre = collection["Nombre"];
                prop.Apellido = collection["Apellido"];
                prop.Dni = collection["Dni"];
                prop.Email = collection["Email"];
                prop.Telefono = collection["Telefono"];
                repositorioPropietario.Modificar(prop);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(null);
            }
        }

        // GET: PropietarioController/Delete/5
        public ActionResult Delete(int id)
        {
            repositorioPropietario.Baja(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: PropietarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
