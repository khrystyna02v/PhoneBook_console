using System.Text.RegularExpressions;

namespace C_Project1
{
    public class Functions
    {
        public static bool CheckPhoneNumber(string? number)
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
        public static bool CheckPartOfName(string? name)
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

        public static string InputName(string? adding = null)
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
        public static string InputSurname(string? adding = null)
        {
            return InputName(adding + "sur");
        }
        public static string InputPhoneNumber(string? adding = null)
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
        public static string? InputEmail()
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
        public static string InputEmail(string adding)
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
        public static string InputPartOfName(string adding)
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

        public static void SortBook(List<Person> phoneBook)
        {
            phoneBook.Sort((person1, person2) => (person1.Name + " " + person1.Surname).CompareTo(person2.Name + " " + person2.Surname));
        }

        public static bool IsInBook(List<Person> phoneBook, string? name, string? surname)
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
            foreach (var person in phoneBook)
            {
                if (person.Email != null)
                {
                    Console.WriteLine($"{counter}) {person.Name} {person.Surname} - {person.PhoneNumber}, {person.Email}");
                }
                else
                {
                    Console.WriteLine($"{counter}) {person.Name} {person.Surname} - {person.PhoneNumber}");
                }
                counter++;
            }
        }

        public static void AddPerson(List<Person> phoneBook)
        {
            Console.WriteLine("Adding new person to the phone book:");
            var name = InputName();
            var surname = InputSurname();
            if (!IsInBook(phoneBook, name, surname))
            {
                var number = InputPhoneNumber();
                var email = InputEmail();
                if (CheckEmail(email))
                {
                    phoneBook.Add(new Person(name, surname, number, email));
                }
                else
                {
                    phoneBook.Add(new Person(name, surname, number));
                }
                Console.WriteLine($"Successfully added to the phone book!");
            }
            else
            {
                Console.WriteLine("This person is alredy in the book");
            }
        }

        public static void ChangeNumber(List<Person> phoneBook)
        {
            Console.WriteLine("Changing a phone number:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                var number = InputPhoneNumber("new ");
                foundPerson.PhoneNumber = number;
                Console.WriteLine($"Phone number successfully changed!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void RemovePerson(List<Person> phoneBook)
        {
            Console.WriteLine("Removing person from the phone book:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                phoneBook.Remove(foundPerson);
                Console.WriteLine($"{name} {surname} successfully removed from the book!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void RenamePerson(List<Person> phoneBook)
        {
            Console.WriteLine("Renaming person:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                var newName = InputName("new ");
                var newSurname = InputSurname("new ");
                foundPerson.Name = newName;
                foundPerson.Surname = newSurname;
                Console.WriteLine($"Successfully renamed!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void ChangeEmail(List<Person> phoneBook)
        {
            Console.WriteLine("Adding/changing email:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                var email = InputEmail("new ");
                foundPerson.Email = email;
                Console.WriteLine($"Successfully changed email!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void DeleteEmail(List<Person> phoneBook)
        {
            Console.WriteLine("Deleting email:");
            var name = InputName();
            var surname = InputSurname();
            var foundPerson = Find(phoneBook, name, surname);
            if (foundPerson != null)
            {
                if (foundPerson.Email == null) Console.WriteLine($"{name} {surname} does not have an email address");
                else
                {
                    foundPerson.Email = null;
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
                    if (person.Email != null)
                    {
                        Console.WriteLine($"{counter}) {person.Name} {person.Surname} - {person.PhoneNumber}, {person.Email}");
                    }
                    else
                    {
                        Console.WriteLine($"{counter}) {person.Name} {person.Surname} - {person.PhoneNumber}");
                    }
                    counter++;
                }
            }
            if (counter == 1)
            {
                Console.WriteLine("Nothing found.");
            }
            Console.WriteLine($"Surnames starting with \"{beginning}\": ");
            counter = 1;
            foreach (var person in phoneBook)
            {
                if (person.Surname.Substring(0, beginning.Length) == beginning)
                {
                    if (person.Email != null)
                    {
                        Console.WriteLine($"{counter}) {person.Name} {person.Surname} - {person.PhoneNumber}, {person.Email}");
                    }
                    else
                    {
                        Console.WriteLine($"{counter}) {person.Name} {person.Surname} - {person.PhoneNumber}");
                    }
                    counter++;
                }
            }
            if (counter == 1)
            {
                Console.WriteLine("Nothing found.");
            }
        }
    }
}
