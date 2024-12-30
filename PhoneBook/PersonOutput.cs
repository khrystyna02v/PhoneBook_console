namespace PhoneBook
{
    public static class PersonOutput
    {
        public static string ConsoleOutput(this Person person)
        {
            if (person.Email != null)
            {
                return $"{person.Name} {person.Surname} - {person.PhoneNumber}, {person.Email}";
            }
            else
            {
                return $"{person.Name} {person.Surname} - {person.PhoneNumber}";
            }
        }
        public static string CsvOutput(this Person person)
        {
            if (person.Email != null)
            {
                return $"{person.Name},{person.Surname},{person.PhoneNumber},{person.Email}\n";
            }
            else
            {
                return $"{person.Name},{person.Surname},{person.PhoneNumber},-\n";
            }
        }
    }
}
