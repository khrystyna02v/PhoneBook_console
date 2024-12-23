using System.Text.RegularExpressions;

namespace C_Project1
{
    public class Functions
    {
        
        public static bool CheckPhoneNumber(string Number)
        {
            Match match = Regex.Match(Number, @"^0[0-9]{9}$");
            return match.Success;
        }
        public static bool CheckName(string Name)
        {
            Match match = Regex.Match(Name, @"^[A-Z]([a-z]+)((\-[A-Z][a-z]+)*)$");
            return match.Success;
        }
        public static bool CheckPartOfName(string Name)
        {
            Match match = Regex.Match(Name, @"^[A-Z]([a-z]*)((\-[A-Z][a-z]+)*)$");
            return match.Success;
        }
        public static bool CheckEmail(string Email)
        {
            if (Email == null) return false;
            Match match = Regex.Match(Email, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9\-]+)\.([a-z]{2,5})(\s*)$");
            return match.Success;
        }

        public static string InputName(string adding = null)
        {
            Console.Write($"Enter {adding}name: ");
            string name = Console.ReadLine();
            while (!CheckName(name))
            {
                Console.WriteLine("Incorrect text format! Try again.");
                Console.Write($"Enter {adding}name: ");
                name = Console.ReadLine();
            }
            return name;
        }
        public static string InputSurname(string adding = null)
        {
            return InputName(adding + "sur");
        }
        public static string InputPhoneNumber(string adding = null)
        {
            Console.Write($"Enter {adding}phone number: ");
            string number = Console.ReadLine();
            while (!CheckPhoneNumber(number))
            {
                Console.WriteLine("It is not a phone number! Try again.");
                Console.Write($"Enter {adding}phone number: ");
                number = Console.ReadLine();
            }
            return number;
        }
        public static string InputEmail()
        {
            Console.Write($"Enter email (not required): ");
            string email = Console.ReadLine();
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
            string email = Console.ReadLine();
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
            string part = Console.ReadLine();
            while (!CheckPartOfName(part))
            {
                Console.WriteLine("Incorrect text format. Try again");
                Console.Write($"Enter {adding} of name/surname: ");
                part = Console.ReadLine();
            }
            return part;
        }






        public static void SortBook(List<Person> PhoneBook)
        {
            PhoneBook.Sort((person1, person2) => (person1.name + " " + person1.surname).CompareTo(person2.name + " " + person2.surname));
        }
        public static bool IsInBook(List<Person> PhoneBook, string Name, string Surname)
        {
            bool found = false;
            foreach (var person in PhoneBook)
            {
                if (Name == person.name && Surname == person.surname) { found = true; break; }
            }
            return found;
        }
        public static Person Find(List<Person> PhoneBook, string Name, string Surname)
        {
            return PhoneBook.Find(person => person.name == Name && person.surname == Surname);
        }

        public static void PrintBook(List<Person> PhoneBook)
        {
            SortBook(PhoneBook);
            Console.WriteLine("Phone Book:");
            int counter = 1;
            foreach (Person person in PhoneBook)
            {
                Console.WriteLine($"{counter}) {person.name} {person.surname} - {person.phonenumber}{person.email}");
                counter++;
            }
        }
        public static void AddPerson(List<Person> PhoneBook)
        {
            Console.WriteLine("Adding new person to the phone book:");
            string name = InputName();
            string surname = InputSurname();
            if (!IsInBook(PhoneBook, name, surname))
            {
                string number = InputPhoneNumber();
                string email = InputEmail();
                PhoneBook.Add(new Person(name, surname, number, email));
                Console.WriteLine($"Successfully added to the phone book!");
            }
            else
            {
                Console.WriteLine("This person is alredy in the book");
            }
        }
        public static void ChangeNumber(List<Person> PhoneBook)
        {
            Console.WriteLine("Changing a phone number:");
            string name = InputName();
            string surname = InputSurname();
            Person foundPerson = Find(PhoneBook, name, surname);
            if (foundPerson!= null)
            {
                string number = InputPhoneNumber("new ");
                foundPerson.phonenumber = number;
                Console.WriteLine($"Phone number successfully changed!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void RemovePerson(List<Person> PhoneBook)
        {
            Console.WriteLine("Removing person from the phone book:");
            string name = InputName();
            string surname = InputSurname();
            Person foundPerson = Find(PhoneBook, name, surname);
            if (foundPerson!= null)
            {
                PhoneBook.Remove(foundPerson);
                Console.WriteLine($"{name} {surname} successfully removed from the book!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void RenamePerson(List<Person> PhoneBook)
        {
            Console.WriteLine("Renaming person:");
            string name = InputName();
            string surname = InputSurname();
            Person foundPerson = Find(PhoneBook, name, surname);
            if (foundPerson != null)
            {
                string newName = InputName("new ");
                string newSurname = InputSurname("new ");
                foundPerson.name = newName;
                foundPerson.surname = newSurname;
                Console.WriteLine($"Successfully renamed!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void ChangeEmail(List<Person> PhoneBook)
        {
            Console.WriteLine("Adding/changing email:");
            string name = InputName();
            string surname = InputSurname();
            Person foundPerson = Find(PhoneBook, name, surname);
            if (foundPerson != null)
            {
                string email = InputEmail("new ");
                foundPerson.email = email;
                Console.WriteLine($"Successfully changed email!");
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void DeleteEmail(List<Person> PhoneBook)
        {
            Console.WriteLine("Deleting email:");
            string name = InputName();
            string surname = InputSurname();
            Person foundPerson = Find(PhoneBook, name, surname);
            if (foundPerson != null)
            {
                if (foundPerson.email == null) Console.WriteLine($"{name} {surname} does not have an email address");
                else
                {
                    foundPerson.email = null;
                    Console.WriteLine($"Email successfully removed!");
                }
            }
            else
            {
                Console.WriteLine($"{name} {surname} is not in the book");
            }
        }
        public static void FirstLetter(List<Person> PhoneBook)
        {
            SortBook(PhoneBook);
            Console.WriteLine("Finding by first letter or beginning of name or surname:");
            string beginning = InputPartOfName("beginning");
            Console.WriteLine($"Names starting with \"{beginning}\": ");
            int counter = 1;
            foreach (Person person in PhoneBook)
            {
                if (person.name.Substring(0, beginning.Length) == beginning)
                {
                    Console.WriteLine($"{counter}) {person.name} {person.surname} - {person.phonenumber}{person.email}");
                    counter++;
                }
            }
            if (counter == 1) {
                Console.WriteLine("Nothing found.");
            }
            Console.WriteLine($"Surnames starting with \"{beginning}\": ");
            counter = 1;
            foreach (Person person in PhoneBook)
            {
                if (person.surname.Substring(0, beginning.Length) == beginning)
                {
                    Console.WriteLine($"{counter}) {person.name} {person.surname} - {person.phonenumber}{person.email}");
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
