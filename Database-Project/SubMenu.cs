// .NET 22 Daniel Svensson
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Project
{
    internal class SubMenu
    {

        SQLCalls sql = new();
        EFCalls ef = new();
        int navigation;
        bool keepLooping, correctInput;


        /*
         * TODO
         * Get all staff from DB SQL
         * Add new staff via SQL 
         */
        public void StaffPortal()
        {
            do
            {
                switch (navigation)
                {
                    default:
                        navigation = 0; keepLooping = true; correctInput = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Staff Portal . Please choose a option");
                            Console.WriteLine("1: Get All Staff From DB");
                            Console.WriteLine("2: Add New Staff");
                            Console.WriteLine("9: Return to Main Menu");
                            int.TryParse(Console.ReadLine(), out navigation); // error checking on input, keeps looping until correct input is found
                            if (navigation == 1 || navigation == 2 || navigation == 3 || navigation == 9)
                            {
                                correctInput = true;
                            }
                            else
                            {
                                Console.Clear(); // clears to not flood the console with same info
                                Console.WriteLine("Wrong input, Try again.");
                            }

                        } while (!correctInput);
                        break;
                    case 1:
                        Console.Clear();
                        sql.GetAllStaff();
                        Thread.Sleep(5000);
                        goto default;
                    case 2:
                        Console.Clear();
                        sql.AddNewStaff();
                        goto default;
                    case 9:
                        keepLooping = false;
                        break;

                }

            } while (keepLooping);
        }
        /*
         * TODO
         * Save all Students with what Year  SQL
         * Save A student with all courses and grades - Teacher and grade date SQL
         * Show info about all Students EF
         */
        public void StudentPortal()
        {
            do
            {
                switch (navigation)
                {
                    default:
                        navigation = 0; keepLooping = true; correctInput = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Student Portal . Please choose a option");
                            Console.WriteLine("1: Get all Students in a certain Year");
                            Console.WriteLine("2: Save Specific student with all courses and grades");
                            Console.WriteLine("3: Show all Students");
                            Console.WriteLine("9: Return to Main Menu");
                            int.TryParse(Console.ReadLine(), out navigation); // error checking on input, keeps looping until correct input is found
                            if (navigation == 1 || navigation == 2 || navigation == 3 || navigation == 9)
                            {
                                correctInput = true;
                            }
                            else
                            {
                                Console.Clear(); // clears to not flood the console with same info
                                Console.WriteLine("Wrong input, Try again.");
                            }

                        } while (!correctInput);
                        break;
                    case 1:
                        Console.Clear();
                        sql.GetStudentsWithYear();
                        goto default;
                    case 2:
                        Console.Clear();
                        sql.SaveStudentWithGrades();
                        goto default;
                    case 3:
                        Console.Clear();
                        ef.ShowAllStudents();
                        goto default;
                    case 9:
                        keepLooping = false;
                        break;

                }

            } while (keepLooping);
        }
        
        /*
         * TODO
         * Number of Teacher's per Department  EF
         * Salary per Department  SQL
         * Average salary per department  SQL
         */
        public void DepartmentPortal()
        {
            do
            {
                switch (navigation)
                {
                    default:
                        navigation = 0; keepLooping = true; correctInput = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Department Portal . Please choose a option");
                            Console.WriteLine("1: Get Teachers per department");
                            Console.WriteLine("2: Salary per department");
                            Console.WriteLine("3: Average salary per department");
                            Console.WriteLine("9: Return to Main Menu");
                            int.TryParse(Console.ReadLine(), out navigation); // error checking on input, keeps looping until correct input is found
                            if (navigation == 1 || navigation == 2 || navigation == 3 || navigation == 9)
                            {
                                correctInput = true;
                            }
                            else
                            {
                                Console.Clear(); // clears to not flood the console with same info
                                Console.WriteLine("Wrong input, Try again.");
                            }

                        } while (!correctInput);
                        break;
                    case 1:
                        Console.Clear();
                        ef.TeachersPerDepartment();
                        goto default;
                    case 2:
                        Console.Clear();
                        sql.SalaryPerDepartment();
                        goto default;

                    case 3:
                        Console.Clear();
                        sql.AverageSalaryPerDepartment();
                        goto default;
                    case 9:
                        keepLooping = false;
                        break;

                }

            } while (keepLooping);
        }

        /*
         * TODO
         * List all Active Courses  EF
         */
        public void CoursesPortal()
        {
            do
            {
                switch (navigation)
                {
                    default:
                        navigation = 0; keepLooping = true; correctInput = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Courses Portal . Please choose a option");
                            Console.WriteLine("1: Show all Active Courses");
                            Console.WriteLine("9: Return to Main Menu");
                            int.TryParse(Console.ReadLine(), out navigation); // error checking on input, keeps looping until correct input is found
                            if (navigation == 1 || navigation == 9)
                            {
                                correctInput = true;
                            }
                            else
                            {
                                Console.Clear(); // clears to not flood the console with same info
                                Console.WriteLine("Wrong input, Try again.");
                            }

                        } while (!correctInput);
                        break;
                    case 1:
                        Console.Clear();
                        ef.ListAllCourses();
                        goto default;
                    case 9:
                        keepLooping = false;
                        break;

                }

            } while (keepLooping);
        }

    }
}
