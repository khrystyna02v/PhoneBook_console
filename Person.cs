namespace C_Project1
{
    public class Person
    {
        private string Name;
        private string Surname;
        private string PhoneNumber;
        private string Email;

        public Person(string name, string surname, string phoneNumber, string email = null)
        {
            this.Name = name;
            this.Surname = surname;
            this.PhoneNumber = phoneNumber;
            if (Functions.CheckEmail(email))
            {
                this.Email = email;
            }
            else
            {
                this.Email = null;
            }
        }

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        public string surname
        {
            get { return Surname; }
            set { Surname = value;}
        }
        public string phonenumber
        {
            get { return PhoneNumber; }
            set { PhoneNumber = value;}
        }
        public string email
        {
            get { if (Email != null) { return ", " + Email; } else { return null; } }
            set { Email = value; }
        }  
    }
}
