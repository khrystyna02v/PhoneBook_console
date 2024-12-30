namespace PhoneBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select which file to use:");
            Console.WriteLine("1 - Csv file");
            Console.WriteLine("2 - Json file");
            IManager manager;
            bool selected = false;
            bool csv = false;
            while (!selected)
            {
                string choice = Console.ReadLine().Replace(" ", "");
                switch (choice)
                {
                    case "1": csv = true; selected = true; break;
                    case "2": selected = true; break;
                    default: Console.WriteLine("Incorrect choice format! Try again."); break;
                }
            }
            if (csv) manager = new CsvManager("../../../Book1.csv");
            else manager = new JsonManager("../../../Book2.json");
            Console.WriteLine();

            var phoneBook = manager.Read();
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
                    case "2": manager.AddPerson(phoneBook); break;
                    case "3": manager.ChangeNumber(phoneBook); break;
                    case "4": manager.RemovePerson(phoneBook); break;
                    case "5": manager.RenamePerson(phoneBook); break;
                    case "6": manager.ChangeEmail(phoneBook); break;
                    case "7": manager.DeleteEmail(phoneBook); break;
                    case "8": Functions.FirstLetter(phoneBook); break;
                    case "9": exit = true; break;
                    default: Console.WriteLine("Incorrect choice format."); break;
                }
                Console.WriteLine();
            }
        }
    }
}