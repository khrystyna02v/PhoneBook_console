namespace C_Project1
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }

        public Person(string name, string surname, string phoneNumber, string? email = null)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
