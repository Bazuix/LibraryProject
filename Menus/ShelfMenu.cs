using LibraryProject.Models.Library;
using LibraryProject.Models.Users;
using LibraryProject.utils;
using static LibraryProject.Models.Library.Books;


namespace LibraryProject.Menus
{
    public class ShelfsMenu
    {
        // Menu Półek
       public static class ShelfManager
        {
            public delegate void MenuAction(string message);
            //Zdarzenie wywołane po zakończeniu operacji
            public static event MenuAction? OnAction;

            public static void ShelfMain(int which)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Edycja półki:");

                    Role role = LogInSystem.user?.Role ?? Role.Client;

                    // Wyświetla menu jęsli użytkownik jest menedżerem
                    if (role == Role.Manager)
                    {
                        Console.WriteLine("1. Wyświetl wszystkie książki");
                        Console.WriteLine("2. Znajdź książkę po ID");
                        Console.WriteLine("3. Filtruj książki");
                        Console.WriteLine("4. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 4))
                        {
                            case 1:
                                DisplayAllBooks(which);
                                break;
                            case 2:
                                FindBook(which);
                                break;
                            case 3:
                                FilterBooksMenu();
                                break;
                            case 4:
                                Console.Clear();
                                return;
                        }
                    }
                    // Wyświetla menu jęsli użytkownik jest adminem
                    else
                    {
                        Console.WriteLine("1. Dodaj książkę");
                        Console.WriteLine("2. Przenieś książkę między półkami");
                        Console.WriteLine("3. Wyświetl wszystkie książki");
                        Console.WriteLine("4. Znajdź książkę po ID");
                        Console.WriteLine("5. Filtruj książki");
                        Console.WriteLine("6. Zmień informacje o książce");
                        Console.WriteLine("7. Usuń książkę");
                        Console.WriteLine("8. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 8))
                        {
                            case 1:
                                AddBook(which);
                                break;
                            case 2:
                                MoveBook(which);
                                break;
                            case 3:
                                DisplayAllBooks(which);
                                break;
                            case 4:
                                FindBook(which);
                                break;
                            case 5:
                                FilterBooksMenu();
                                break;
                            case 6:
                                ChangeBook(which);
                                break;
                            case 7:
                                RemoveBook(which);
                                break;
                            case 8:
                                Console.Clear();
                                return;
                        }
                    }
                }
            }
            // Metoda dodająca książkę
            public static void AddBook(int which)
            {
                Console.Clear();

                Books.Book book;

                Console.WriteLine("Wybierz typ:");
                Console.WriteLine("1.Fantasy");
                Console.WriteLine("2.Thriller");
                int type = MainMenuManager.SelectNumber(1, 2);

                Console.Clear();

                Console.Write("Podaj ilość stron: ");
                int countInBook = MainMenuManager.SelectNumber(1, 9999);

                Console.Clear();

                string typeObject = MainMenuManager.SelectText("Podaj tytuł książki: ");

                Console.Clear();

                // Tworzy książkę o podanym typie z podanymi wartościami 
                if (type == 1)
                {
                    book = new Books.BookFantasy(Library.shelfs[which].Books.Count, which, countInBook, typeObject, LogInSystem.user ?? new User("root",Role.Client));
                }
                else
                {
                    book = new Books.BookThriller(Library.shelfs[which].Books.Count, which, countInBook, typeObject,LogInSystem.user ?? new User("root", Role.Client));
                }

                // Dodaje książkę do półki
                Library.shelfs[which].AddBook(book);
                Logger.SendLog("Dodał książkę");

                // Zdarzenie kończy operację i wyświetla komunikat
                OnAction?.Invoke("Książka została dodana");

                return;
            }
            // Metoda przesuwa książkę na inną półkę
            private static void MoveBook(int which)
            {
                Console.Clear();

                // Sprawdzenie czy podana półka jest pusta
                if (Library.shelfs[which].Books.Count == 0)
                {
                    OnAction?.Invoke("Brak książek na tej półce!");
                    return;
                }

                Console.Write($"Wybierz książkę(x{Library.shelfs[which].Books.Count}): ");
                int id_book = MainMenuManager.SelectNumber(1, Library.shelfs[which].Books.Count) - 1;

                Console.Clear();

                Console.WriteLine("Wybierz nową półkę: ");
                int new_id_shelf= MainMenuManager.SelectNumber(1, Library.shelfs.Count) - 1;
                Library.MoveBook(which, new_id_shelf, id_book);

                Console.Clear();
                Logger.SendLog($"Przeniosł książkę z półki #{which} na półkę #{new_id_shelf}");

                OnAction?.Invoke("książka została przeniesiona");
                return;

            }
            // Metoda wyświetlająca wszystkie książki
            private static void DisplayAllBooks(int which)
            {
                Console.Clear();

                if (Library.shelfs[which].Books.Count == 0)
                {
                    OnAction?.Invoke("Brak książek na tej półce!");
                    return;
                }

                Library.shelfs[which].DisplayInfo();

                OnAction?.Invoke("");
                return;
            }
            // Metoda wyszukująca daną książkę po ID
            public static void FindBook(int which)
            {
                Console.Clear();

                if (Library.shelfs[which].Books.Count == 0)
                {
                    OnAction?.Invoke("Brak książek na tej półce!");
                    return;
                }

                // Pobiera od użytkownika indeks książki
                Console.WriteLine($"Wybierz książkę(x{Library.shelfs[which].Books.Count}): ");
                int whichBook = MainMenuManager.SelectNumber(1, Library.shelfs[which].Books.Count) - 1;
                Console.WriteLine();

                // Sprawdzenie czy użytkownik może zobaczyć informację o książce
                if (Library.shelfs[which].Books[whichBook].owner.UserName == LogInSystem.user?.UserName || LogInSystem.user?.Role != Role.Client)
                {
                    Library.shelfs[which].Books[whichBook].ShowInfo();
                }
                else 
                {
                    Console.WriteLine($"Książka nie należy do {LogInSystem.user?.UserName}");
                }
                OnAction?.Invoke("");
                return;
            }
            // Metoda zmieniająca informację o książce
            private static void ChangeBook(int which)
            {
                if (Library.shelfs[which].Books.Count == 0)
                {
                    OnAction?.Invoke("Brak książek na tej półce!");
                    return;
                }

                // Pobiera od użytkownika wartości książki
                Console.Write($"Wybierz książkę(x{Library.shelfs[which].Books.Count}): ");
                int selectedBook_toChange = MainMenuManager.SelectNumber(1, Library.shelfs[which].Books.Count) - 1;
                Console.Clear();

                string objectsName = MainMenuManager.SelectText("Podaj tytuł książki: ");
                Console.Clear();

                Console.Write("Podaj ilość stron: ");
                int count = MainMenuManager.SelectNumber(1, 9999);
                Console.Clear();

                // Zmienia książkę
                Library.shelfs[which].Books[selectedBook_toChange].ChangeInfo(objectsName, count);
                Logger.SendLog($"Zmienił dane książki #{selectedBook_toChange} z półki #{which}");

                OnAction?.Invoke("Książka została zmieniona");
                return;
            }
            // Metoda usuwająca książkę
            private static void RemoveBook(int which)
            {
                if (Library.shelfs[which].Books.Count == 0)
                {
                    OnAction?.Invoke("Brak książki na tej półce!");
                    return;
                }

                // Pobieranie od użytkownika indeksu książki i usuwania jej
                Console.Write($"Wybierz książkę(x{Library.shelfs[which].Books.Count}): ");
                int whichBook = MainMenuManager.SelectNumber(1, Library.shelfs[which].Books.Count) - 1;
                Library.shelfs[which].RemoveBook(whichBook);
                Console.Clear();
                Console.WriteLine();

                Logger.SendLog($"Usunął książkę #{whichBook} z półki #{which}");
                OnAction?.Invoke("Książka została usunięta");
                return;
            }
            //Menu do filtrowania książek
            private static void FilterBooksMenu()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Menu filtrowania książek:");
                    Console.WriteLine("1. Filtruj według tytułu");
                    Console.WriteLine("2. Filtruj według typu");
                    Console.WriteLine("3. Filtruj według ilości stron");
                    Console.WriteLine("4. Wróć");

                    switch (MainMenuManager.SelectNumber(1, 4))
                    {
                        case 1:
                            //Filtrowanie po tytule
                            Console.Clear();
                            string title = MainMenuManager.SelectText("Podaj tytuł książki:");
                            ShowFilteredBooks(Filter.FilterByTitle(title));
                            break;
                        case 2:
                            //Filtrowanie po typie
                            Console.Clear();
                            string type = MainMenuManager.SelectText("Podaj typ książki (np. Fantasy, Thriller):");
                            ShowFilteredBooks(Filter.FilterByType(type));
                            break;
                        case 3:
                            //Filtrowanie po ilości stron
                            Console.Clear();
                            int pageCount = MainMenuManager.SelectNumber(1, 9999);
                            ShowFilteredBooks(Filter.FilterByPageCount(pageCount));
                            break;
                        case 4:
                            return;
                    }

                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
            }
            //Metoda pokazująca przefiltrowane książki
            private static void ShowFilteredBooks(List<Book> books)
            {
                Console.Clear();
                if (books.Count == 0)
                {
                    Console.WriteLine("Brak książek spełniających kryteria.");
                }
                else
                {
                    Console.WriteLine($"Znaleziono {books.Count} książek:");
                    Filter.DisplayBooks(books);
                }
            }


        }
    }
}
