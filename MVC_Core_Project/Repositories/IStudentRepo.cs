using MVC_Core_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_Project.Repositories
{
    public interface IStudentRepo
    {
        List<Student> Get();
        Student Get(int id);
        Student GetWithParent(int id);
        List<Subject> GetSubjectList();
        bool Insert(Student s);
        bool Update(Student s);
        bool Delete(int id);
    }
}
