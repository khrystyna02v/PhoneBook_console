namespace PhoneBook
{
    public class Person(string name, string surname, string phoneNumber, string? email = null)
    {
        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string? Email { get; set; } = email;
    }
}
