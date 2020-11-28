using MVC_Core_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_Project.Repositories
{
    public interface IDepartmentRepo
    {

        List<Department> Get();
        List<Department> GetWithChildred();
        Department Get(int id);
        bool Insert(Department department);
        bool Update(Department department, bool childIncluded = false);
        bool Delete(int id);
    }
}
