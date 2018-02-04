using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace williamfaglie.MovieLib.Host
{
    class Program
    {
        static string _title;
        static int _length;
        static string _description;
        static bool _owned;

        static void Main( string[] args )
        {
            bool exit = false;
            while (!exit)
            {

                //Displays menu
                char choice = Menu();

                // Process menu selection
                switch (Char.ToUpper(choice))
                {
                    //case 'l':
                    case 'L':
                    ListMovie();
                    break;

                    //case 'a':
                    case 'A':
                    AddMovie();
                    break;

                    //case 'r':
                    case 'R':
                    //RemoveMovie();
                    break;

                    //case 'e':
                    case 'E':
                    exit = true;
                    break;
                };
            };

        }

        static void AddMovie()
        {
            //Get title
            _title = ReadString("Enter title: ", true);

            //Get description
            _description = ReadString("Enter an optional description: ", false);

            //Get length
            _length = ReadInt("Enter the optional length (in minutes): ", 0, false);

            //Get Owned
            _owned = ReadBool("Do you own this movie? (Y/N) ", true);
        }

        private static bool ReadBool( string message, bool isRequired )
        {
            do
            {
                Console.Write(message);

                string input = Console.ReadLine();

                input = input.Trim();

                input = input.ToUpper();

                //'y':
                if (input == "Y")
                {
                    Console.WriteLine("Status = Owned");
                    return false;
                }

                //'n':
                else if (input == "N")
                {
                    Console.WriteLine("Status = On Wishlist");
                    return false;
                }
                

                    Console.WriteLine("Please choose a valid option.");
                
                
            } while (true);
        }

        private static int ReadInt( string message, int minValue, bool isRequired)
        {
            do
            {
                Console.Write(message);

                string value = Console.ReadLine();

                if (int.TryParse(value, out int result))
                {
                    //If not required or not empty
                    if (result >= minValue)
                        return result;
                    else if (!isRequired)
                        return minValue;
                };

                string msg = String.Format("Value must be >= {0}", minValue);
                Console.WriteLine(msg);
            } while (true);
        }

        private static string ReadString( string message, bool isRequired)
        {
            do
            {
                Console.Write(message);

                string value = Console.ReadLine();

                //If not required or not empty
                if (!isRequired || value != "")
                    return value;

                Console.WriteLine("Value is required");
            } while (true);
        }

        private static char Menu()
        {
            do
            {
                Console.WriteLine("L)ist movies");
                Console.WriteLine("A)dd a new movie");
                Console.WriteLine("R)emove an existing movie");
                Console.WriteLine("E)xit the application");

                string input = Console.ReadLine();

                input = input.Trim();

                input = input.ToUpper();

                if (String.Compare(input, "L", true) == 0)
                    return input[0];
                else if (input == "A")
                    return input[0];
                else if (input == "R")
                    return input[0];
                else if (input == "E")
                    return input[0];

                Console.WriteLine("Please choose a valid option");
            } while (true);
        }

        static void ListMovie()
        {
            if (!String.IsNullOrEmpty(_title))
            {
                string msg = $"{_title} \n {_description} \n" + "Run length = " + $"{_length}" + " mins \n" + ;
                Console.WriteLine(msg);

                if (!String.IsNullOrEmpty(_description))
                    Console.WriteLine(_description);

            } else
                Console.WriteLine("No Movies");
        }
    }
}
