namespace LibraryProject.Models.Library
{
        // Podstawowa klasa biblioteki
        public static class Library
        {
            // Lista wszystkich półek
            public static List<Shelf> shelfs = new List<Shelf>();

            // Metoda zmieniająca położenie książki
            public static void MoveBook(int id_shelf, int new_id_shelf, int id_book)
            {
                try
                {
                    // Sprawdzenie czy książka już na wybranej półce
                    if (id_shelf == new_id_shelf)
                    {
                        Console.WriteLine("Książka już jest na tej półce");
                        return;
                    }

                    // Zmiana półki książki
                    Books.Book book = shelfs[id_shelf].Books[id_book];
                    shelfs[id_shelf].Books.Remove(book);
                    book.MoveBook(new_id_shelf);
                    shelfs[new_id_shelf].Books.Add(book);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Nie prawidłowy indeks półki lub książki: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
                }
            }
            // Metoda usuwająca półkę
            public static void RemoveShelf(int id)
            {
                // Usuwa wszystkie książki i półkę
                shelfs[id].Books.Clear();
                shelfs.RemoveAt(id);

                //Zmienia indeks półki w książkach
                for (int i = 0; i < shelfs.Count; i++)
                {
                    shelfs[i].id_Shelf = i;
                    foreach (Books.Book book in shelfs[i].Books)
                    {
                        book.id_shelf = i;
                    }
                }
            }
            // Metoda dodająca nową książkę
            public static void AddBook(Books.Book book, int id_shelf)
            {
                // Zmienia półkę książki i dodaje ją na wybraną półkę
                book.MoveBook(id_shelf);
                shelfs[id_shelf].AddBook(book);
            }
            // Metoda wyświetlająca informację o wszystkich półkach
            public static void DisplayAllShelfs()
            {
                foreach (Shelf shelf in shelfs)
                {
                    shelf.DisplayInfo();
                }
            }
        }
}

