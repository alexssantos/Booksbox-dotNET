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
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _client.GetLivroByIdAsync(id);

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<LivroDetails>();
                return View(model);
            }

            ViewBag.Error = "Livro não Encontrado";
            return View();
        }

        // POST: Livros/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, LivroDetails model)
        {
            try
            {
                var response = await _client.PutLivroAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Livros/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _client.GetLivroByIdAsync(id);

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<LivroDetails>();
                return View(model);
            }
            return View();
        }

        // POST: Livros/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, LivroDetails model)
        {
            try
            {
                var response = await _client.DeleteLivroAsync(model.id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }            
        }
    }
}
