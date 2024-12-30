namespace PhoneBook
{
    public static class CsvManager
    {
        public static List<Person> Read(string source)
        {
            var phoneBook = new List<Person>();
            if (!File.Exists(source))
            {
                using (File.Create(source)) { }
                return phoneBook;
            }
            using (var file = File.OpenRead(source))
            {
                var array = new byte[file.Length];
                file.Read(array);
                var textFromFile = System.Text.Encoding.Default.GetString(array);
                var data = textFromFile.Split(',', '\n');
                if (!Functions.CheckName(data[0]))
                {
                    data[0] = data[0].Substring(1);
                }
                for (var i = 0; i + 3 < data.Length; i += 4)
                {
                    if (Functions.CheckEmail(data[i + 3]))
                    {
                        phoneBook.Add(new Person(data[i], data[i + 1], data[i + 2], data[i + 3]));
                    }
                    else
                    {
                        phoneBook.Add(new Person(data[i], data[i + 1], data[i + 2]));
                    }
                }
            }
            return phoneBook;
        }
        public static void Add (string source, Person person)
        {
            var text = person.CsvOutput();
            using (var file = new StreamWriter(source, append: true, encoding: System.Text.Encoding.UTF8))
            {
                file.WriteLine(text);
            }
        }

        public static void Rewrite (string source, List<Person> phoneBook)
        {
            using (var file = new StreamWriter(source, append: false, encoding: System.Text.Encoding.UTF8))
            {
                foreach (var person in phoneBook)
                {
                    file.Write(person.CsvOutput());
                }
            }
        }
    }
}
