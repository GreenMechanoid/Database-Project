// .NET 22 Daniel Svensson
using Database_Project.Data;
using Database_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Database_Project
{
    internal class EFCalls
    {


        public void ShowAllStudents()
        {
            Console.WriteLine("Fetching all Students....");
            using (var context = new ProjectContext()) 
            {
                var Query = from stud in context.Students
                               select stud;
                foreach (var stud in Query)
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Student Name:" + stud.Firstname + " " + stud.LastName + " In Year: " 
                        + stud.ClassYear +" With Student ID: "+ stud.StudentId + "\n");
                }
            }
        }

        public void TeachersPerDepartment()
        {

            using (var context = new ProjectContext())
            {
                var Query = from Sta in context.staff
                               join dept in context.Departments on Sta.DepartmentId equals dept.DepartmentId
                               where Sta.Occupation == "Teacher"
                               group dept by dept.DepartmentName into c

                               select new 
                               {
                                  deptName = c.Key,
                                  TeachCount = c.Count()
                               };

                foreach (var dept in Query)
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Department: " + dept.deptName + "\nNumber of Teachers = " + dept.TeachCount);
                }
            }

        }

        public void ListAllCourses()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Showing all ACTIVE Courses");
            Console.WriteLine("---------------------------");

            using (var context = new ProjectContext())
            {
                var Query = from course in context.Courses
                            join sta in context.staff on course.StaffId equals sta.StaffId
                            where course.IsActive == 1
                            select new 
                            {
                                CourseName = course.CourseName,
                                Teachname = sta.FirstName + " " + sta.LastName 
                            };

                foreach (var item in Query)
                {
                    Console.WriteLine("Course Name: " + item.CourseName + " Teacher: " + item.Teachname);
                }
            }
            Console.WriteLine("\n Do you wish to see the Inactive classes aswell? (Y)es or anything for no");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {

                Console.WriteLine("---------------------------");
                Console.WriteLine("Showing all INACTIVE Courses");
                Console.WriteLine("---------------------------");

                using (var context = new ProjectContext())
                {
                    var Query = from course in context.Courses
                                where course.IsActive == 0
                                select new
                                {
                                    CourseName = course.CourseName,
                                };

                    foreach (var item in Query)
                    {
                        Console.WriteLine("\nCourse Name: " + item.CourseName);
                    }
                }
            }
            else
            {
                Console.WriteLine("Returning...");
            }
        }
    }
}
