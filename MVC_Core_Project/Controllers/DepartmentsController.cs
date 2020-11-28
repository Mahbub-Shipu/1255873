using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Core_Project.Models;
using MVC_Core_Project.Repositories;
using X.PagedList;

namespace MVC_Core_Project.Controllers
{
    public class DepartmentsController : Controller
    {
        IDepartmentRepo repo;
        public DepartmentsController(IDepartmentRepo repo) { this.repo = repo; }
        public IActionResult Index(int page = 1)
        {
            int size = 5;
            var data = repo.GetWithChildred()
                .ToPagedList(page, size);

            return View(data);
        }
        public IActionResult CreateDepartmentWithSubject()
        {
            return View();
        }
        public IActionResult CreateDepartmentWithSubject1()
        {
            return View();
        }
        public IActionResult CreateDepartmentWithSubject2()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateDepartmentWithSubject([FromBody]Department dept)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(dept))
                    return Json(new { success = true });
                else
                    return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }
            //return View();
        }
        public IActionResult GetSubjectAddForm()
        {
            return PartialView("_SubjectAddRowPartial");
        }
        public IActionResult Create(string postBack = "")
        {
            if (postBack != "")
            {
                ViewBag.PostBack = "Success";
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(dept))
                {
                    return RedirectToAction("Create", new { postBack = "postBack" });
                }
            }
            return View(dept);
        }
        public IActionResult EditWithSubject(int id)
        {
            var data = repo.Get(id);
            if (data == null)
                return NotFound();
            return View(data);
        }
        [HttpPost]
        public IActionResult EditWithSubject([FromBody]Department dept)
        {
            if (ModelState.IsValid)
            {
                repo.Update(dept);
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }
        public IActionResult Delete(int id)
        {
            return View(repo.Get(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}