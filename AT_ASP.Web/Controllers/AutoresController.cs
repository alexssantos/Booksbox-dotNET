using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp_Api.Controllers.HttpClientApi;
using AT_ASP.Web.Models;

namespace AT_ASP.Web.Controllers
{
    [Authorize]
    public class AutoresController : Controller
    {
        private readonly HttpApiClient _client = new HttpApiClient();           

        // GET: Autores
        public async Task<ActionResult> Index()
        {

            var response = await _client.GetAutoresAsync();

            if (response.IsSuccessStatusCode)
            {
                var models = await response.Content.ReadAsAsync<IEnumerable<AutorViewModel>> ();

                return View(models);
            }

            return View("Error",new List<AutorViewModel>());
        }

        // GET: Autores/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _client.GetAutorAsync(id);

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<AutorDetails>();
                ViewData["LivrosDoAutor"] = model.LivrosEscritos;

                return View("Details",model);
            }

            return View("Error");
        }

        // GET: Autores/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        public async Task<ActionResult> Create(AutorDetails model)
        {
            try
            {                
                //Create
                await _client.PostAutorAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Autores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Autores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Autores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Autores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
