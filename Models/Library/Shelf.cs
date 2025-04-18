namespace LibraryProject.Models.Library
{
        public class Shelf
        {
            // Podstawowe wartości półki
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

        // Dodaje książkę do półki
        public void AddBook(Books.Book book)
            {
                Books.Add(book);
            }
            // Usuwa książkę z półki
            public void RemoveBook(int id)
            {
                Books.RemoveAt(id);
                for(int i = 0; i < Books.Count; i++)
                {
                    Books[i].id = i;
                }
            }
            // Wyświetla informację o wszystkich książkach
            public void DisplayInfo()
            {
                Console.WriteLine($"Półka #{id_Shelf+1}");
                Console.WriteLine("Zawartość:");

                //Sprawdzenie czy półka jest pusta
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
