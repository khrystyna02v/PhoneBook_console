using System.Text.RegularExpressions;

namespace PhoneBook
{
    public static class Functions
    {
        private static bool CheckPhoneNumber(string? number)
        {
            if (number == null) return false;
            var match = Regex.Match(number, @"^0[0-9]{9}$");
            return match.Success;
        }
        public static bool CheckName(string? name)
        {
            if (name == null) return false;
            var match = Regex.Match(name, @"^[A-Z]([a-z]+)((\-[A-Z][a-z]+)*)$");
            return match.Success;
        }
        private static bool CheckPartOfName(string? name)
        {
            if (name == null) return false;
            var match = Regex.Match(name, @"^[A-Z]([a-z]*)((\-[A-Z][a-z]+)*)$");
            return match.Success;
        }
        public static bool CheckEmail(string? email)
        {
            if (email == null) return false;
            var match = Regex.Match(email, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9\-]+)\.([a-z]{2,5})(\s*)$");
            return match.Success;
        }

        private static string InputName(string? adding = null)
        {
            Console.Write($"Enter {adding}name: ");
            var name = Console.ReadLine();
            while (!CheckName(name))
            {
                Console.WriteLine("Incorrect text format! Try again.");
                Console.Write($"Enter {adding}name: ");
                name = Console.ReadLine();
            }
            return name;
        }
        private static string InputSurname(string? adding = null)
        {
            return InputName(adding + "sur");
        }
        private static string InputPhoneNumber(string? adding = null)
        {
            Console.Write($"Enter {adding}phone number: ");
            var number = Console.ReadLine();
            while (!CheckPhoneNumber(number))
            {
                Console.WriteLine("It is not a phone number! Try again.");
                Console.Write($"Enter {adding}phone number: ");
                number = Console.ReadLine();
            }
            return number;
        }
        private static string? InputEmail()
        {
            Console.Write($"Enter email (not required): ");
            var email = Console.ReadLine();
            if (!CheckEmail(email))
            {
                Console.WriteLine("Can't add this email address");
                return null;
            }
            return email;
        }
        private static string InputEmail(string adding)
        {
            Console.Write($"Enter {adding}email: ");
            var email = Console.ReadLine();
            while (!CheckEmail(email))
            {
                Console.WriteLine("Incorrect email address! Try again.");
                Console.Write($"Enter {adding}email: ");
                email = Console.ReadLine();
            }
            return email;
        }
        private static string InputPartOfName(string adding)
        {
            Console.WriteLine($"Enter {adding} of name/surname: ");
            var part = Console.ReadLine();
            while (!CheckPartOfName(part))
            {
                Console.WriteLine("Incorrect text format. Try again");
                Console.Write($"Enter {adding} of name/surname: ");
                part = Console.ReadLine();
            }
            return part;
        }

        private static void SortBook(List<Person> phoneBook)
        {
            phoneBook.Sort((person1, person2) => (person1.Name + " " + person1.Surname).CompareTo(person2.Name + " " + person2.Surname));
        }

        private static bool IsInBook(List<Person> phoneBook, string? name, string? surname)
        {
            var found = false;
            foreach (var person in phoneBook)
            {
                if (name == person.Name && surname == person.Surname) { found = true; break; }
            }
            return found;
        }

        public static Person? Find(List<Person> phoneBook, string? name, string? surname)
        {
            return phoneBook.Find(person => person.Name == name && person.Surname == surname);
        }

        public static void PrintBook(List<Person> phoneBook)
        {
            SortBook(phoneBook);
            Console.WriteLine("Phone Book:");
            var counter = 1;
            foreach (var person in phoneBook) Console.WriteLine($"{counter++}) {person.ConsoleOutput()}");
        }

        public static void AddPerson(this IManager manager, List<Person> phoneBook)
        {
            Console.WriteLine("Adding new person to the phone book:");
            var name = InputName();
            var surname = InputSurname();
            if (!IsInBook(phoneBook, name, surname))
            {
                var number = InputPhoneNumber();
                var email = InputEmail();
                phoneBook.Add(new Person(name, surname, number, email));
                manager.Add(new Person(name, surname, number, email));
                Console.WriteLine($"Successfully added to the phone book!");
            }
            else
            {
                Console.WriteLine("This person is alredy in the book");
            }
        }

        public static void ChangeNumber(this IManager manager, List<Person> phoneBook)
        {
            Console.WriteLine("Changing a phone number:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                var number = InputPhoneNumber("new ");
                if (foundPerson.PhoneNumber != number)
                {
                    foundPerson.PhoneNumber = number;
                    manager.Rewrite(phoneBook);
                    Console.WriteLine($"Phone number successfully changed!");
                }
                else
                {
                    Console.WriteLine("You entered the same phone number");
                }
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void RemovePerson(this IManager manager, List<Person> phoneBook)
        {
            Console.WriteLine("Removing person from the phone book:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                phoneBook.Remove(foundPerson);
                manager.Rewrite(phoneBook);
                Console.WriteLine($"{name} {surname} successfully removed from the book!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void RenamePerson(this IManager manager, List<Person> phoneBook)
        {
            Console.WriteLine("Renaming person:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                var newName = InputName("new ");
                var newSurname = InputSurname("new ");
                if (foundPerson.Name != newName || foundPerson.Surname != newSurname)
                {
                    foundPerson.Name = newName;
                    foundPerson.Surname = newSurname;
                    manager.Rewrite(phoneBook);
                    Console.WriteLine("Successfully renamed!");
                }
                else
                {
                    Console.WriteLine("You entered the same name and surname");
                }
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void ChangeEmail(this IManager manager, List<Person> phoneBook)
        {
            Console.WriteLine("Adding/changing email:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                var email = InputEmail("new ");
                if (foundPerson.Email != email)
                {
                    foundPerson.Email = email;
                    manager.Rewrite(phoneBook);
                    Console.WriteLine("Successfully changed email!");
                }
                else
                {
                    Console.WriteLine("You entered the same email");
                }
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void DeleteEmail(this IManager manager, List<Person> phoneBook)
        {
            Console.WriteLine("Deleting email:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                if (foundPerson.Email == null)
                {
                    Console.WriteLine($"{name} {surname} does not have an email address");
                }
                else
                {
                    foundPerson.Email = null;
                    manager.Rewrite(phoneBook);
                    Console.WriteLine($"Email successfully removed!");
                }
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void FirstLetter(List<Person> phoneBook)
        {
            SortBook(phoneBook);
            Console.WriteLine("Finding by first letter or beginning of name or surname:");
            string beginning = InputPartOfName("beginning");
            Console.WriteLine($"Names starting with \"{beginning}\": ");
            var counter = 1;
            foreach (var person in phoneBook)
            {
                if (person.Name.Substring(0, beginning.Length) == beginning)
                {
                    Console.WriteLine($"{counter++}) {person.ConsoleOutput()}");
                }
            }
            if (counter == 1) Console.WriteLine("Nothing found.");
            Console.WriteLine($"Surnames starting with \"{beginning}\": ");
            counter = 1;
            foreach (var person in phoneBook)
            {
                if (person.Surname.Substring(0, beginning.Length) == beginning)
                {
                    Console.WriteLine($"{counter++}) {person.ConsoleOutput()}");
                }
            }
            if (counter == 1) Console.WriteLine("Nothing found.");
        }
    }
}
