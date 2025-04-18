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
            // Metoda wyświetlająca informację o książce
            public virtual void ShowInfo()
            {
                Console.WriteLine($"id: {id}, półka: {id_shelf + 1}, tytuł: {objectsName}, stron: {count}");
            }
            // Metoda zmieniająca informację o książce
            public void ChangeInfo(string objectsName, int count)
            {
                this.objectsName = objectsName;
                this.count = count;
            }
            // Metoda zmieniająca pozycję książki
            public void MoveBook(int id_shelf)
            {
                this.id_shelf = id_shelf;
            }
        }
        public class BookFantasy : Book
        {
            public string FantasyTitle { get; private set; }
            public BookFantasy(int id, int id_shelf, int count, string FantasyTitle, User owner) : base(id, FantasyTitle, count,owner)
            {
                this.FantasyTitle = FantasyTitle;
            }
            public override void ShowInfo()
            {
                base.ShowInfo();
                Console.WriteLine($"Fantasy o tytule: {FantasyTitle}");
            }
        }
        public class BookThriller : Book
        {
            public string ThrillerTitle { get; private set; }
            public BookThriller (int id, int id_shelf,int count, string ThrillerTitle, User owner) : base(id, ThrillerTitle, count, owner)
            {
                this.ThrillerTitle = ThrillerTitle;
            }
            public override void ShowInfo()
            {
                base.ShowInfo();
                Console.WriteLine($"Thriller o tytule: {ThrillerTitle}");
            }
        }
    }
}
