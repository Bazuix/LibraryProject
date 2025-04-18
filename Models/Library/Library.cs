namespace LibraryProject.Models.Library
{
        // Podstawowa klasa magazynu
        public static class Library
        {
            // Lista wszystkich sekcji
            public static List<Shelf> shelfs = new List<Shelf>();

            // Metoda zmieniająca położenie pudełka
            public static void MoveBook(int id_shelf, int new_id_shelf, int id_book)
            {
                try
                {
                    // Sprawdzenie czy pudełko już jest w wybranej sekcsji
                    if (id_shelf == new_id_shelf)
                    {
                        Console.WriteLine("Książka już jest na tej półce");
                        return;
                    }

                    // Zmiana sekcji pudełka
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
            // Metoda usuwająca sekcję
            public static void RemoveShelf(int id)
            {
                // Usuwa wszystkie pudełka i sekcję
                shelfs[id].Books.Clear();
                shelfs.RemoveAt(id);

                //Zmienia indeks sekcji w pudełkach
                for (int i = 0; i < shelfs.Count; i++)
                {
                    shelfs[i].id_Shelf = i;
                    foreach (Books.Book book in shelfs[i].Books)
                    {
                        book.id_shelf = i;
                    }
                }
            }
            // Metoda dodająca nowe pudełko
            public static void AddBook(Books.Book book, int id_shelf)
            {
                // Zmienia sekcję pudełka i dodaje go do wybranej sekcji
                book.MoveBook(id_shelf);
                shelfs[id_shelf].AddBook(book);
            }
            // Metoda wyświetlająca informację o wszystkich sekcjach
            public static void DisplayAllShelfs()
            {
                foreach (Shelf shelf in shelfs)
                {
                    shelf.DisplayInfo();
                }
            }
        }
}

