namespace PhoneBook
{
    public interface IManager
    {
        List<Person> Read();
        void Add(Person person);
        void Rewrite(List<Person> phoneBook);
    }
}
