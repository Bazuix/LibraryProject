namespace LibraryProject.Models.Library
{
        public class Shelf
        {
            // Podstawowe wartości sekcji
            public int id_Shelf { get; set; }
            public List<Books.Book> Books = new List<Books.Book>();

            // Przypisuje id przy tworzeniu
            public Shelf(int id)
            {
                id_Shelf = id;
            }

        public Shelf()
        {
        }

        // Dodaje pudełko do sekcji
        public void AddBook(Books.Book book)
            {
                Books.Add(book);
            }
            // Usuwa pudełko z sekcji
            public void RemoveBook(int id)
            {
                Books.RemoveAt(id);
                for(int i = 0; i < Books.Count; i++)
                {
                    Books[i].id = i;
                }
            }
            // Wyświetla informację o wszystkich pudełkach
            public void DisplayInfo()
            {
                Console.WriteLine($"Półka #{id_Shelf+1}");
                Console.WriteLine("Zawartość:");

                //Sprawdzenie czy jest sekcja pusta
                if(Books.Count == 0)
                {
                    Console.WriteLine("Półka jest pusta");
                    return;
                }
                foreach(Books.Book book in Books)
                {
                    book.ShowInfo();
                }
            }
        }

    
}
