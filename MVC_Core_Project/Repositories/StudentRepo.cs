using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Core_Project.Models;

namespace MVC_Core_Project.Repositories
{
    public class StudentRepo : IStudentRepo
    {
        SubjectDbContext db;
        public StudentRepo(SubjectDbContext db) { this.db = db; }
        public List<Student> Get()
        {
            return db.Students
                 .Include(s => s.Subject)
                 .ThenInclude(c => c.Department)
                 .ToList();
        }

        public Student Get(int id)
        {
            return db.Students.FirstOrDefault(x => x.StudentId == id);
        }
        public Student GetWithParent(int id)
        {
            return db.Students.Include(x => x.Subject).FirstOrDefault(x => x.StudentId == id);
        }
        public List<Subject> GetSubjectList()
        {
            return db.Subjects.ToList();
        }
        public bool Insert(Student s)
        {
            db.Students.Add(s);
            return db.SaveChanges() > 0;
        }

        public bool Update(Student s)
        {
            db.Entry(s).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Entry(new Student { StudentId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;

        }
    }
}
