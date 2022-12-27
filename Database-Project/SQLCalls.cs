// .NET 22 Daniel Svensson
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Project
{
    internal class SQLCalls
    {
        SqlCommand cmd;
        SqlConnection conn = new("Data Source=GREENMECHANOID;" + "Initial Catalog=ProjectDB;" + "Trusted_Connection=true");
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
            SQLQuery = "SELECT FirstName, LastName, Occupation, Hiredate FROM Staff";
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

        public void GetStudentsWithYear()
        {

        }

        public void SaveStudentWithGrades()
        {

        }

        public void SalaryPerDepartment()
        {

        }

        public void AverageSalaryPerDepartment()
        {

        }


        public void TryConnection(string SQLCountString, string SQLDataSelection)
        {
            SqlDataReader dr; // Middle hand for calls between C# and SQL Select request
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
                        Console.WriteLine("Name: {0} {1} \n Occupation {2}\nHired on: {3}\n-----------", dr[0], dr[1], dr[2], dr[3]);
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
