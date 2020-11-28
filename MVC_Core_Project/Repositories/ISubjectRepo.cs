using MVC_Core_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_Project.Repositories
{
    public interface ISubjectRepo
    {
        List<Subject> Get();
        List<Subject> GetWithChild();
        Subject Get(int id);
        Subject GetWithChild(int id);
        List<Department> GetDepartmentsList(); //for dopdown
        List<Subject> GetSubjectOptions(int departId);//for dopdown
        bool Insert(Subject s);
        bool Update(Subject s);
        bool UpdateWithChild(Subject s);
        bool Delete(int id);
        bool InsertStudent(Student[] students);
    }
}
