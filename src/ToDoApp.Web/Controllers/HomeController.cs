using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Models;
using ToDoApp.Domain.Entities;
using ToDoApp.Web.Models;

namespace ToDoApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoService _service;

        public HomeController(IToDoService service)
        {
            _service = service;
        }

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;

            ViewBag.Categories = _service.GetCategories();
            ViewBag.Statuses = _service.GetStatuses();
            ViewBag.DueFilters = Filters.DueFilterValues;

            var query = new ToDoQuery
            {
                CategoryId = filters.CategoryId,
                Due = filters.Due,
                StatusId = filters.StatusId
            };

            var tasks = _service.GetTasks(query);

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _service.GetCategories();
            ViewBag.Statuses = _service.GetStatuses();
            var task = new ToDo { StatusId = "open" };
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                _service.AddTask(task);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _service.GetCategories();
            ViewBag.Statuses = _service.GetStatuses();
            return View(task);
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            _service.MarkComplete(selected.Id);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            _service.DeleteCompleted();
            return RedirectToAction("Index", new { ID = id });
        }
    }
}
