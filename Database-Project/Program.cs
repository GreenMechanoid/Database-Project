// .NET 22 Daniel Svensson
using Microsoft.EntityFrameworkCore;

namespace Database_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // There's no mention of needing a ER Diagram in the Assignment info, Hence no ER Diagram
            SubMenu menu = new();
            bool keepLooping = true, correctInput = false;
            int navigation = 0;
            do
            {

                switch (navigation)
                {
                    default:
                        navigation = 0; keepLooping = true; correctInput = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Main Menu . Please choose a option");
                            Console.WriteLine("1: Staff Portal");
                            Console.WriteLine("2: Student Portal");
                            Console.WriteLine("3: Department Portal");
                            Console.WriteLine("4: Courses Portal");
                            Console.WriteLine("9: Exit program.");
                            int.TryParse(Console.ReadLine(), out navigation); // error checking on input, keeps looping until correct input is found
                            if (navigation == 1 || navigation == 2 || navigation == 3 || navigation == 4 || navigation == 9)
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
                        menu.StaffPortal();
                        goto default;
                    case 2:
                        Console.Clear();
                        menu.StudentPortal();
                        goto default;
                    case 3:
                        Console.Clear();
                        menu.DepartmentPortal();
                        goto default;
                    case 4:
                        Console.Clear();
                        menu.CoursesPortal();
                        goto default;
                    case 9:
                        keepLooping = false;
                        break;
                }


            } while (keepLooping); // infinite loop until user wants to exit
        }
    }
}