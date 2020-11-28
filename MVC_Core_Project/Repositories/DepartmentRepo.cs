using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Core_Project.Models;

namespace MVC_Core_Project.Repositories
{
    public class DepartmentRepo : IDepartmentRepo
    {
        SubjectDbContext db;
        public DepartmentRepo(SubjectDbContext db)
        {
            this.db = db;
        }

        public List<Department> Get()
        {
            return db.Departments.ToList();
        }
        public List<Department> GetWithChildred()
        {
            return db.Departments
                 .Include(x => x.Subjects)
                 .ThenInclude(y => y.Students)
                 .ToList();
        }
        public Department Get(int id)
        {
            return db.Departments
                .Include(x => x.Subjects)
                .FirstOrDefault(x => x.DepartmentId == id);
        }
        public bool Insert(Department department)
        {
            db.Departments.Add(department);
            return db.SaveChanges() > 0;
        }

        public bool Update(Department department, bool childIncluded = false)
        {
            var orignal = db.Departments.Include(x => x.Subjects).First(x => x.DepartmentId == department.DepartmentId);
            orignal.DepartmentName = department.DepartmentName;
            if (department.Subjects != null && department.Subjects.Count > 0)
            {
                var subjects = department.Subjects.ToArray();
                for (var i = 0; i < subjects.Length; i++)
                {
                    var temp = orignal.Subjects.FirstOrDefault(c => c.SubjectId == subjects[i].SubjectId);
                    if (temp != null)
                    {
                        temp.SubjectName = subjects[i].SubjectName;
                        temp.Duration = subjects[i].Duration;

                    }
                    else
                    {
                        orignal.Subjects.Add(subjects[i]);
                    }
                }
                foreach (var c in orignal.Subjects)
                {
                    var temp = department.Subjects.FirstOrDefault(t => t.SubjectId == c.SubjectId);
                    if (temp == null)
                        db.Entry(c).State = EntityState.Deleted;
                }
            }

            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Entry(new Department { DepartmentId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
    }
}
