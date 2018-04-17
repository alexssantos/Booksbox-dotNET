using AT_ASP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp_Api.Controllers.HttpClientApi;

namespace AT_ASP.Web.Controllers
{
    public class LivrosController : Controller
    {

        private readonly HttpApiClient _client = new HttpApiClient();

        // GET: Livros
        public async Task<ActionResult> Index()
        {
            var response = await _client.GetLivrosAsync();

            if (response.IsSuccessStatusCode)
            {
                var models = await response.Content.ReadAsAsync<IEnumerable<LivroViewModel>>();

                return View(models);
            }

            return View("Error", new List<LivroViewModel>());
        }

        // GET: Livros/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _client.GetLivroByIdAsync(id);

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<LivroDetails>();
                ViewData["AutoresDoLivro"] = model.AutoresDoLivro;

                return View("Details", model);
            }

            return View("Error");
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        public async Task<ActionResult> Create(LivroDetails model)
        {
            try
            {
                await _client.PostLivroAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Livros/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Livros/Edit/5
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

        // GET: Livros/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Livros/Delete/5
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
