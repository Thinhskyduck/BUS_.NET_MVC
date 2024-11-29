
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEDU.Areas.Admin.Models;
using System.Threading.Tasks;
using TEDU.Services;

namespace TEDU.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsAPIService _newsApiService;

        public NewsController(NewsAPIService apiService)
        {
            _newsApiService = apiService;
        }

        //Index
        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "News";
            var news = await _newsApiService.GetNewsAsync();
            var newss = await NewList();
            return View(newss);
        }

        [HttpGet]
        public async Task<IEnumerable<News>> NewList()
        {

            var news = await _newsApiService.GetNewsAsync();
            if (news == null || !news.Any())
            {
                TempData["Message"] = "No news found.";
            }
            return news;
        }

        //Create New
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PageType = "News";
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(News newNew)
        {
            if (ModelState.IsValid)
            {
                var result = await _newsApiService.CreateNewAsync(newNew);
                if (result == "Create successfully")
                {
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newNew);
        }


        //Update Route
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            // Lấy thông tin chi tiết tin tức để hiển thị
            var routes = await _newsApiService.GetNewsAsync();
            var route = routes?.FirstOrDefault(r => r.Id == id);

            if (route == null)
            {
                return HttpNotFound();
            }

            return View(route);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Update(int id, News updatedNew)
        {
            if (ModelState.IsValid)
            {
                var result = await _newsApiService.UpdateNewAsync(id, updatedNew);
                if (result == "Update successfully")
                {
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updatedNew);
        }

        //Delete Route
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            // Lấy thông tin chi tiết tin tức để xác nhận xóa
            var routes = await _newsApiService.GetNewsAsync();
            var route = routes?.FirstOrDefault(r => r.Id == id);

            if (route == null)
            {
                return HttpNotFound();
            }

            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteNewsConfirmed(int id)
        {
            var result = await _newsApiService.DeleteNewAsync(id);
            if (result == "Delete successfully")
            {
                return RedirectToAction("Index", "News");
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("Delete", "News", new { id });
            }
        }

        // Controller trong Area Admin
        
        public async Task<ActionResult> Details(int id)
        {
            var newsList = await _newsApiService.GetNewsAsync();
            var news = newsList.FirstOrDefault(n => n.Id == id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View("Details", "News", news);
        }


    }
}