using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Core_Project.Models
{
    public class Department
    {
        public Department()
        {
            this.Subjects = new List<Subject>();
        }
        [Display(Name ="Department Id")]
        public int DepartmentId { get; set; }
        [Required,StringLength(30),Display(Name="Department Name")]
        public string DepartmentName { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
    public class Subject
    {
        public Subject()
        {
            this.Students = new List<Student>();
        }
        [Display(Name = "Subject ID")]
        public int SubjectId { get; set; }
        [Required, StringLength(40), Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
        [Required, Display(Name = "Duration (Hrs)")]
        public int Duration { get; set; }
        [Required, ForeignKey("Department"), Display(Name = "Department")]
        public int DepartmentId { get; set; }
        //
        public virtual Department Department { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Student
    {
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Required, Column(TypeName = "date"), DataType(DataType.Date), Display(Name = "Date of Birth"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        [Required, EmailAddress, StringLength(40)]
        public string Email { get; set; }
        [Required, StringLength(50), Display(Name = "Address")]
        public string Address { get; set; }
        [Required, ForeignKey("Subject"), Display(Name = "Subject")]
        public int SubjectId { get; set; }
        //
        public virtual Subject Subject { get; set; }
    }
    public class SubjectDbContext : DbContext
    {
        public SubjectDbContext(DbContextOptions<SubjectDbContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
