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
    public class StudentsController : Controller
    {
        IStudentRepo repo;
        public StudentsController(IStudentRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int page = 1, int subjectId = 0)
        {

            var list = repo.GetSubjectList();
            list.Insert(0, new Subject { SubjectId = 0, SubjectName = "All" });
            ViewBag.Subjects = list;
            int pageSize = 5;
            var data = repo.Get();

            if (subjectId > 0)
            {
                return View(data.Where(s => s.SubjectId == subjectId).ToPagedList(page, pageSize));
            }
            return View(data.ToPagedList(page, pageSize));
        }
        public IActionResult Create(string postBack = "")
        {
            var list = repo.GetSubjectList();
            ViewBag.Subjects = list;
            if (postBack != "")
            {
                ViewBag.PostBack = "PostBack";
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(s))
                {
                    return RedirectToAction("Create", new { postBack = "PostBack" });
                }
            }
            var list = repo.GetSubjectList();
            ViewBag.Subjects = list;

            return View(s);
        }
        public IActionResult Edit(int id, string postBack = "")
        {
            var list = repo.GetSubjectList();
            ViewBag.Subjects = list;
            if (postBack != "")
            {
                ViewBag.PostBack = "PostBack";
            }
            var student = repo.Get(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                if (repo.Update(s))
                {
                    return RedirectToAction("Edit", new { id = s.StudentId, postBack = "PostBack" });
                }
            }
            var list = repo.GetSubjectList();
            ViewBag.Subjects = list;


            return View(s);
        }
        public IActionResult Delete(int id)
        {
            return View(repo.GetWithParent(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}