using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Core_Project.Models;

namespace MVC_Core_Project.Repositories
{
    public class SubjectRepo : ISubjectRepo
    {
        SubjectDbContext db;
        public SubjectRepo(SubjectDbContext db) { this.db = db; }
        public List<Subject> Get()
        {
            return db.Subjects.ToList();
        }
        public List<Subject> GetWithChild()
        {
            return db.Subjects
                .Include(x => x.Department)
                .Include(x => x.Students)
                .ToList();
        }
        public Subject Get(int id)
        {
            return db.Subjects.FirstOrDefault(x => x.SubjectId == id);
        }
        public Subject GetWithChild(int id)
        {
            return db.Subjects
                .Include(x => x.Department)
                .Include(x => x.Students)
                .FirstOrDefault(x => x.SubjectId == id);
        }
        public List<Subject> GetSubjectOptions(int departId)
        {
            return db.Subjects.Where(c => c.DepartmentId == departId).ToList();
        }
        public List<Department> GetDepartmentsList()
        {
            return db.Departments.ToList();
        }

        public bool Insert(Subject s)
        {
            db.Subjects.Add(s);
            return db.SaveChanges() > 0;
        }

        public bool InsertStudent(Student[] students)
        {
            db.Students.AddRange(students);
            return db.SaveChanges() > 0;
        }

        public bool Update(Subject s)
        {
            db.Entry(s).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool UpdateWithChild(Subject s)
        {
            var orignal = db.Subjects.Include(x => x.Students).First(x => x.SubjectId == s.SubjectId);
            orignal.DepartmentId = s.DepartmentId;
            orignal.SubjectName = s.SubjectName;
            orignal.Duration = s.Duration;

            if (s.Students != null && s.Students.Count > 0)
            {
                var students = s.Students.ToArray();
                for (var i = 0; i < students.Length; i++)
                {
                    var temp = orignal.Students.FirstOrDefault(x => x.StudentId == students[i].StudentId);
                    if (temp != null)
                    {
                        temp.StudentName = students[i].StudentName;
                        temp.Birthday = students[i].Birthday;
                        temp.Email = students[i].Email;
                        temp.Address = students[i].Address;
                    }
                    else
                    {
                        orignal.Students.Add(students[i]);
                    }
                }
                var originalStudents = orignal.Students.ToArray();
                for (var i = 0; i < originalStudents.Length; i++)
                {
                    var temp = s.Students.FirstOrDefault(t => t.StudentId == originalStudents[i].StudentId);
                    if (temp == null)
                        db.Students.Remove(originalStudents[i]);
                }
            }
            //db.Entry(c).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Entry(new Subject { SubjectId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
    }
}
