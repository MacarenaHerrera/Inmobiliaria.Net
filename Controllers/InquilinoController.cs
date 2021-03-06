using Inmobiliaria.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Controllers
{
   public class InquilinoController : Controller
        {
            private readonly RepositorioInquilino repositorioInquilino;
            private readonly IConfiguration config;
            public InquilinoController(IConfiguration config)
            {
                repositorioInquilino = new RepositorioInquilino();
                this.config = config;
            }
            // GET: InquilinoController
            public ActionResult Index()
            {
                var lista = repositorioInquilino.Obtener();
                ViewData[nameof(Inquilino)] = lista;
                return View(lista);
            }

            // GET: InquilinoController/Details/5
            public ActionResult Details(int id)
            {
                return View();
            }

            // GET: InquilinoController/Create
            public ActionResult Crear()
            {
                return View();
            }

            // POST: InquilinoController/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Crear(Inquilino inquilino)
            {
                try
                {
                    repositorioInquilino.Alta(inquilino);
                    TempData["Id"] = inquilino.Id;
                    return RedirectToAction(nameof(Index));
                    //return View();
                }
                catch (Exception ex)
                {
                    return View();
                }
            }

            // GET: InquilinoController/Edit/5
            public ActionResult Editar(int id)
            {
                var inquilino = repositorioInquilino.ObtenerInquilino(id);
                return View(inquilino);
            }

            // POST: InquilinoController/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Editar(int id, IFormCollection collection)
            {
                try
                {
                    Inquilino inquilino = repositorioInquilino.ObtenerInquilino(id);
                    inquilino.Nombre = collection["Nombre"];
                    inquilino.Apellido = collection["Apellido"];
                    inquilino.Dni = collection["Dni"];
                    inquilino.Email = collection["Email"];
                    inquilino.Telefono = collection["Telefono"];
                    repositorioInquilino.Modificar(inquilino);
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

            // GET: InquilinoController/Delete/5
            public ActionResult Eliminar(int id)
            {
                repositorioInquilino.Baja(id);
                return RedirectToAction(nameof(Index));
            }

            // POST: InquilinoController/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Eliminar(int id, IFormCollection collection)
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
