// .NET 22 Daniel Svensson
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Project.Models;
using System.Text.RegularExpressions;

namespace Database_Project
{
    internal class SQLCalls
    {
        SqlCommand cmd;
        SqlConnection conn = new("Data Source=GREENMECHANOID;" + "Initial Catalog=ProjectDB;" + "Trusted_Connection=true"); // Needs changeing for other Users (Local DB)
        string SQLCountString, SQLQuery;
        public SQLCalls()
        {
            cmd = new()
            {
                CommandTimeout = 60,// if command is endless or unresponsive, it will abort after 60sec , double from normal at 30sec
                Connection = conn,
                CommandType = CommandType.Text
            };
        }


        public void GetAllStaff()
        {
            SQLCountString = "SELECT Count(*) FROM Staff";
            SQLQuery = "SELECT FirstName, LastName, Occupation, Hiredate , FLOOR((CAST (GetDate() AS INTEGER) - CAST(Hiredate AS INTEGER)) / 365.25) as Years FROM Staff";
            TryConnection(SQLCountString,SQLQuery);
        }

        public void AddNewStaff()
        {
            bool keepLooping = true, correctInput = false; 
            string tempString;
            List<String> inputData = new();
            DateOnly hiredate;
            float Salary;
            int deptID;
            do
            {
                if (inputData.Count > 0) inputData.Clear();

                Console.WriteLine("Adding New staff member\n");
                Console.WriteLine("Please Enter FirstName");
                inputData.Add(Console.ReadLine());
                Console.WriteLine("Please enter LastName");
                inputData.Add(Console.ReadLine());
                Console.WriteLine("Please enter Occupation");
                inputData.Add(Console.ReadLine());
                Console.WriteLine("Please enter Hireing Date XXXX-XX-XX (Leave empty for Today)");
                if (DateOnly.TryParse(tempString = Console.ReadLine(), out hiredate))
                {
                    //Success , we do nothing, keep moving     
                }
                else if (tempString == "")
                {
                    //Success , we do nothing, keep moving    
                }
                else
                {
                    //Failure, need to have a Date - try again
                    Console.Clear();
                    do
                    {
                        Console.WriteLine("Wrong input. Needs to be in this format xxxx-xx-xx (Leave empty for Today)");

                        if (DateOnly.TryParse(tempString = Console.ReadLine(), out hiredate))
                        {
                            correctInput = true;
                        }
                        else if (tempString == "")
                        {

                        }
                    } while (!correctInput);
                    correctInput = false;
                }

                SQLCountString = "SELECT Count(*) AS FINDDEPART FROM Department";
                SQLQuery = "SELECT * FROM Department";
                TryConnection(SQLCountString, SQLQuery);

                Console.WriteLine("Enter DepartmentID");

                if (int.TryParse(Console.ReadLine(), out deptID))
                {
                    //Success , we do nothing, keep moving     
                }
                else
                {
                    //Failure, need to have a number - try again
                    Console.Clear();
                    do
                    {
                        Console.WriteLine("Wrong input. Needs to be one of the Following:\n");

                        TryConnection(SQLCountString, SQLQuery);

                        if (int.TryParse(Console.ReadLine(), out deptID))
                        {
                            correctInput = true;
                        }
                    } while (!correctInput);
                    correctInput = false;
                }
                Console.WriteLine("And Finally Enter the Salary");
                if (float.TryParse(Console.ReadLine(), out Salary))
                {
                    //Success , we do nothing, keep moving     
                }
                else
                {
                    //Failure, need to have a number - try again
                    Console.Clear();
                    do
                    {
                        Console.WriteLine("Wrong input. Needs to be Numbers '1234567890'");

                        if (float.TryParse(Console.ReadLine(), out Salary))
                        {
                            correctInput = true;
                        }
                    } while (!correctInput);
                    correctInput = false;
                }

                foreach (var item in inputData)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine( "HireDate: " + hiredate);
                Console.WriteLine("Department: " +deptID);
                Console.WriteLine("Salary " + Salary);

                Console.WriteLine("is this Information correct? Y/N");
                do
                {
                    ConsoleKey tempkey = Console.ReadKey(false).Key;
                    if (tempkey == ConsoleKey.Y)
                    {
                        correctInput = true;
                        keepLooping = false;
                    }
                    else if (tempkey == ConsoleKey.N)
                    {
                        Console.Clear();
                        Console.WriteLine("Let'start from the top then: \n");
                        break;
                    }
                } while (!correctInput);



            } while (keepLooping);

            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    cmd.CommandText = $"INSERT INTO Staff (Staff.FirstName, Staff.LastName, Staff.Occupation, Staff.Hiredate, staff.DepartmentID, Staff.Salary) " +
                        $"VALUES ('{inputData[0].Replace("'", "''")}','{inputData[1].Replace("'", "''")}','{inputData[2].Replace("'", "''")}',{hiredate}, {deptID}, {Salary})";
                    cmd.ExecuteScalar();

                    Console.WriteLine($"Staff member: {inputData[0] + " " + inputData[1]} has been added to the Database");
                }
            }
            catch (Exception exp)
            {

                Console.WriteLine(exp.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        public void GetStudentsYearList()
        {
            int input = 0;
            do
            {
                Console.WriteLine("input SchoolYear: 1 ,2 or 3");
            } while (!int.TryParse(Console.ReadLine(),out input));
            SQLCountString = $"SELECT Count(*) FROM Students AS StudentLists WHERE ClassYear = {input}";
            SQLQuery = $"SELECT FirstName,LastName,ClassYear From Students WHERE ClassYear = {input}";
            TryConnection(SQLCountString, SQLQuery);
        }

        public void SaveStudentWithGrades()
        {
            int StudID;
            SQLCountString = "SELECT Count(*) FROM Students AS Stud1";
            SQLQuery = "SELECT FirstName,LastName,StudentID FROM Students Order By StudentID";
            TryConnection(SQLCountString, SQLQuery);
            Console.WriteLine("Please Select the Student you wish to print the grades for");
            do
            {

            } while (!int.TryParse(Console.ReadLine(),out StudID));

            SQLCountString = $"SELECT Count(*) From Grades AS Stud2 Where StudentID = {StudID}";
            SQLQuery = $"SELECT Students.FirstName ,Students.LastName ,Staff.FirstName, Staff.LastName, Course.CourseName, Grades.Grade,Grades.GradeDate " +
                $"FROM Students " +
                $"INNER JOIN Grades ON Students.StudentID = Grades.StudentID " +
                $"INNER JOIN Staff ON Grades.StaffID = Staff.StaffID " +
                $"INNER JOIN Course ON Course.StaffID = Staff.StaffID WHERE Students.StudentID = {StudID}";
            TryConnection(SQLCountString, SQLQuery);
        }

        public void SalaryPerDepartment()
        {
            SQLCountString = "SELECT Count(*) FROM DEPARTMENT AS SalaryPerDept";
            SQLQuery = "SELECT Sum(Salary), DepartmentName FROM Staff INNER JOIN Department on Department.DepartmentID = Staff.DepartmentID GROUP BY DepartmentName";
            TryConnection(SQLCountString, SQLQuery);

        }

        public void AverageSalaryPerDepartment() 
        {
            SQLCountString = "SELECT Count(*) FROM DEPARTMENT AS SalaryPerDept";
            SQLQuery = "SELECT Avg(Salary), DepartmentName FROM Staff INNER JOIN Department on Department.DepartmentID = Staff.DepartmentID GROUP BY DepartmentName";
            TryConnection(SQLCountString, SQLQuery);
        }


        public void TryConnection(string SQLCountString, string SQLDataSelection)
        {
            //i should've changed this entire method into using string interpolation with $"" as i find it easier to work with.
            //but to keep it consistent with previous assignment i've chosen not to

            SqlDataReader dr; // Middle hand for calls between C# and SQL Select request
            bool once = false; // used in save to file parts
            try
            {
                cmd.CommandText = SQLCountString;
                conn.Open();
                //If Statement Checks incoming string for what table it's about
                if (conn.State == ConnectionState.Open && SQLCountString.Contains("FROM Staff") == true)
                {
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    // For loop to read everything from the table  
                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read(); // Read one row from the table  
                        Console.WriteLine("Name: {0} {1} \n Occupation {2}\nHired on: {3} , and has been with us for {4} Years\n-----------", dr[0], dr[1], dr[2], dr[3], dr[4]);
                    }
                }

                else if (conn.State == ConnectionState.Open && SQLCountString.Contains("AS FINDDEPART") == true)
                {
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    // For loop to read everything from the table  
                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read(); // Read one row from the table  
                        Console.WriteLine("DepartmentID: {0} Department Name: {1}", dr[0], dr[1]);
                    }
                }
                else if (conn.State == ConnectionState.Open && SQLCountString.Contains("AS SalaryPerDept") == true)
                {
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    // For loop to read everything from the table  
                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read(); // Read one row from the table  
                        Console.WriteLine("Salary Total: {0} For Department: {1}", dr[0], dr[1]);
                    }
                }
                else if (conn.State == ConnectionState.Open && SQLCountString.Contains("AS StudentLists") == true)
                {
                    
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    // For loop to read everything from the table  
                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read(); // Read one row from the table  
                        Console.WriteLine("FirstName: {0} LastName: {1} SchoolYear: {2}", dr[0], dr[1], dr[2]);

                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClassList.txt") && once == false)
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClassList.txt");
                            once = true;

                        }
                        using (StreamWriter sw = new(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClassList.txt",append:true))
                        {

                            sw.WriteLine(dr[0].ToString() + ", " + dr[1].ToString() + " SchoolYear: " + dr[2].ToString());
                        }
                    }
                    Console.WriteLine("File of Students and SchoolYear save to: {0}", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClassList.txt");
                }
                else if (conn.State == ConnectionState.Open && SQLCountString.Contains("AS Stud1") == true)  
                    // Getting the full list of students in SQL to do Student With Grades
                {
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    // For loop to read everything from the table  
                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read(); // Read one row from the table  
                        Console.WriteLine("Name: {0} {1}  Student ID: {2}", dr[0], dr[1], dr[2]);
                    }
                }
                else if (conn.State == ConnectionState.Open && SQLCountString.Contains("From Grades AS Stud2") == true)
                {
                    string savedName="";
                    object objCount = cmd.ExecuteScalar();
                    int iCount = (int)objCount;
                    cmd.CommandText = SQLDataSelection;
                    dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    // For loop to read everything from the table  
                    for (int i = 0; i < iCount; i++)
                    {
                        dr.Read(); // Read one row from the table  
                        if (i == 0)
                        {
                        Console.WriteLine("Name: {0} {1}", dr[0], dr[1]);

                        }
                        else
                        {
                            Console.WriteLine("Course: {0}, Grade: {1} \n Grading Teacher: {2} {3} , Grading Date {4}",dr[4], dr[5], dr[2], dr[3], dr[6]);
                        }
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + (@$"\{dr[0]}_{dr[1]}.txt")) && once == false)
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + (@$"\{dr[0]}_{dr[1]}.txt"));
                            once = true;

                        }
                        using (StreamWriter sw = new(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + (@$"\{dr[0]}_{dr[1]}.txt"), append: true))
                        {
                            if (once == false)
                            {
                                savedName = dr[0].ToString() + " " + dr[1].ToString();
                                sw.WriteLine("Student Name: {0} {1}" + dr[0].ToString() + dr[1].ToString());
                            }

                            sw.WriteLine("Course: " + dr[4].ToString() + " Grade: " + dr[5].ToString() + " Teacher: " + dr[2].ToString() + " " + dr[3].ToString() + " Grading Date: " + dr[6].ToString());
                        }
                    }
                    Console.WriteLine($"File Of {dr[0]}_{dr[1]}'s Grades saved to {Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + (@$"\{dr[0]}_{dr[1]}.txt")}");
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
