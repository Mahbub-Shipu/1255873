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
    public class SubjectsController : Controller
    {
        ISubjectRepo repo;
        public SubjectsController(ISubjectRepo repo)
        {

            this.repo = repo;
        }
        public IActionResult Index(int page = 1, int departId = 0)
        {
            var list = repo.GetDepartmentsList();
            list.Insert(0, new Department { DepartmentId = 0, DepartmentName = "All" });
            ViewBag.Departments = list;
            int pageSize = 5;
            var data = repo.GetWithChild();
            if (departId > 0)
            {
                return View(data.Where(x => x.DepartmentId == departId).ToPagedList(page, pageSize));
            }
            return View(data.ToPagedList(page, pageSize));


        }
        public IActionResult Create()
        {
            var list = repo.GetDepartmentsList();
            list.Insert(0, new Department { DepartmentId = 0, DepartmentName = "Select" });
            ViewBag.Departments = list;
            return View();
        }
        public IActionResult GetSubjectOptions(int id)
        {
            var data = repo.GetSubjectOptions(id);
            data.Insert(0, new Subject { SubjectId = 0, SubjectName = "Select" });
            return Json(data);
        }
        public IActionResult SaveStudents([FromBody]Student[] students)
        {
            if (ModelState.IsValid)
            {
                repo.InsertStudent(students);
                return Json(new { success = true, data = "Saved" });
            }
            return Json(new { success = false, data = "Error" });
        }
        public IActionResult CreateSingle(string postBack = "")
        {
            var list = repo.GetDepartmentsList();
            
            ViewBag.Departments = list;
            if (postBack != "")
            {
                ViewBag.PostBack = "Success";
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateSingle(Subject s)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(s))
                {
                    return RedirectToAction("CreateSingle", new { postBack = "postBack" });
                }

            }
            var list = repo.GetDepartmentsList();
            
            ViewBag.Departments = list;
            return View(s);
        }
        public IActionResult EditSingle(int id, string postBack = "")
        {
            var list = repo.GetDepartmentsList();
            
            ViewBag.Departments = list;
            if (postBack != "")
            {
                ViewBag.PostBack = "Success";
            }
            var data = repo.Get(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditSingle(Subject s)
        {
            if (ModelState.IsValid)
            {
                if (repo.Update(s))
                {
                    return RedirectToAction("EditSingle", new { postBack = "postBack" });
                }

            }
            var list = repo.GetDepartmentsList();
            ViewBag.Departments = list;
            return View(s);
        }
        public IActionResult Edit(int id)
        {
            var list = repo.GetDepartmentsList();
            
            ViewBag.Trades = list;
            var data = repo.GetWithChild(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult SaveCourse([FromBody]Subject s)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateWithChild(s);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult Delete(int id)
        {
            return View(repo.GetWithChild(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}