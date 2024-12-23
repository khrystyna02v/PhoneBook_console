namespace C_Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var PhoneBook = new List<Person>();
            const int fields = 4;
            using (FileStream file = File.OpenRead("Book1.csv"))
            {
                byte[] array = new byte[file.Length];
                file.Read(array);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                var data = textFromFile.Split(',', '\n');
                for (var i = fields; i + fields - 1 < data.Length; i += fields)
                {
                    PhoneBook.Add(new Person(data[i], data[i + 1], data[i + 2], data[i + 3]));
                }
            }

            bool Exit = false;
            while (!Exit)
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
                    case "1": Functions.PrintBook(PhoneBook); break;
                    case "2": Functions.AddPerson(PhoneBook); break;
                    case "3": Functions.ChangeNumber(PhoneBook); break;
                    case "4": Functions.RemovePerson(PhoneBook); break;
                    case "5": Functions.RenamePerson(PhoneBook); break;
                    case "6": Functions.ChangeEmail(PhoneBook); break;
                    case "7": Functions.DeleteEmail(PhoneBook); break;
                    case "8": Functions.FirstLetter(PhoneBook); break;
                    case "9": Exit = true; break;
                    default: Console.WriteLine("Incorrect choice format."); break;
                }
                Console.WriteLine();
            }
        }
    }
}