namespace C_Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = CSV_Reader.Read("Book1.csv");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Select what to do with the phone book:");
                Console.WriteLine("1 - Print phone book");
                Console.WriteLine("2 - Add new person");
                Console.WriteLine("3 - Change phone number");
                Console.WriteLine("4 - Remove a person");
                Console.WriteLine("5 - Rename a person");
                Console.WriteLine("6 - Change or add email");
                Console.WriteLine("7 - Delete email");
                Console.WriteLine("8 - Find by first letter or beginning of word");
                Console.WriteLine("9 - Exit");

                string choice = Console.ReadLine().Replace(" ", "");
                Console.WriteLine();
                switch (choice)
                {
                    case "1": Functions.PrintBook(phoneBook); break;
                    case "2": Functions.AddPerson(phoneBook); break;
                    case "3": Functions.ChangeNumber(phoneBook); break;
                    case "4": Functions.RemovePerson(phoneBook); break;
                    case "5": Functions.RenamePerson(phoneBook); break;
                    case "6": Functions.ChangeEmail(phoneBook); break;
                    case "7": Functions.DeleteEmail(phoneBook); break;
                    case "8": Functions.FirstLetter(phoneBook); break;
                    case "9": exit = true; break;
                    default: Console.WriteLine("Incorrect choice format."); break;
                }
                Console.WriteLine();
            }
        }
    }
}