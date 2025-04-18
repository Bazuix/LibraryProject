using LibraryProject.Interfaces;
using LibraryProject.Models.Users;

namespace LibraryProject.Models.Library
{
    public class Books
    {
        public abstract class Book : IBook
        {
            public int id { get; set; }
            public int id_shelf { get; set; }
            public string objectsName { get; set; }
            public int count { get; set; }
            private User _owner;

            // Obiekt User zwraca wartość _owner. Przypisuje wartość tylko jeśli użytkownik jest klientem
            public User owner
            {
                get
                {
                    return _owner;
                }
                set
                {
                    if (value.Role == Role.Client)
                    {
                        _owner = value;
                    }
                    else
                    {
                        throw new ArgumentException("Właściciel musi mieć rolę Klienta");
                    }
                }
            }
            // Przypisuje podstawowe wartości przy tworzeniu
            public Book(int id, string objectsName, int count, User owner)
            {
                this.id = id;
                this.objectsName = objectsName;
                this.count = count;
                _owner = owner;
            }
            // Metoda wyświetlająca informację o pudełku
            public virtual void ShowInfo()
            {
                Console.WriteLine($"id: {id}, półka: {id_shelf + 1}, tytuł: {objectsName}, stron: {count}");
            }
            // Metoda zmieniająca informację o pudełku
            public void ChangeInfo(string objectsName, int count)
            {
                this.objectsName = objectsName;
                this.count = count;
            }
            // Metoda zmieniająca pozycję pudełka
            public void MoveBook(int id_shelf)
            {
                this.id_shelf = id_shelf;
            }
        }
        public class BookFantasy : Book
        {
            public string FantasyType { get; private set; }
            public BookFantasy(int id, int id_shelf, int count, string FantasyType, User owner) : base(id, FantasyType, count,owner)
            {
                this.FantasyType = FantasyType;
            }
            public override void ShowInfo()
            {
                base.ShowInfo();
                Console.WriteLine($"Typ fantasy: {FantasyType}");
            }
        }
        public class BookThriller : Book
        {
            public string ThrillerType { get; private set; }
            public BookThriller (int id, int id_shelf,int count, string ThrillerType, User owner) : base(id, ThrillerType, count, owner)
            {
                this.ThrillerType = ThrillerType;
            }
            public override void ShowInfo()
            {
                base.ShowInfo();
                Console.WriteLine($"Typ thrilleru: {ThrillerType}");
            }
        }
    }
}
