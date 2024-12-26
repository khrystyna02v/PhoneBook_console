namespace C_Project1
{
    public static class CSV_Reader
    {
        public static List<Person> Read(string source)
        {
            var phoneBook = new List<Person>();
            const int fields = 4;
            if (!File.Exists(source))
            {
                using (File.Create(source)) { }
            }
            using (FileStream file = File.OpenRead(source))
            {
                byte[] array = new byte[file.Length];
                file.Read(array);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                var data = textFromFile.Split(',', '\n');
                for (var i = fields; i + fields - 1 < data.Length; i += fields)
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
    }
}
