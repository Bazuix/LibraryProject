using LibraryProject.Models.Library;
using LibraryProject.Models.Users;
using LibraryProject.utils;

namespace LibraryProject.Menus
{
    public class LibraryMenu
    {
        public static class LibraryManager
        {
            
            public delegate void MenuAction(string message);
            //Zdarzenie wywołane po zakończeniu operacji
            public static event MenuAction? OnAction;

            public static void LibraryMain()
            {
                while (true)
                {
                    Console.WriteLine("Zarządzanie biblioteką:");

                    // Wyświetla menu jęsli użytkownik jest klientem
                    Role role = LogInSystem.user?.Role ?? Role.Client;
                    if (role == Role.Client)
                    {
                        Console.WriteLine("1. Dodaj książkę");
                        Console.WriteLine("2. Znajdź książkę po ID");
                        Console.WriteLine("3. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 3))
                        {
                            case 1:
                                int result = SelectShelf();

                                // Sprawdza obecność sekcji
                                if (result == -1)
                                {
                                    break;
                                }
                                else
                                {
                                    ShelfsMenu.ShelfManager.AddBook(result);
                                }

                                break;
                            case 2:
                                int Result = SelectShelf();

                                // Sprawdza obecność sekcji
                                if (Result == -1)
                                {
                                    break;
                                }
                                else
                                {
                                    ShelfsMenu.ShelfManager.FindBook(Result);
                                }
                                break;
                            case 3:
                                Console.Clear();
                                return;
                        }
                    }
                    // Wyświetla menu jęsli użytkownik jest menedżerem
                    else if (role == Role.Manager)
                    {
                        Console.WriteLine("1. Edytuj półkę");
                        Console.WriteLine("2. Wyświetl wszystkie półki");
                        Console.WriteLine("3. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 3))
                        {
                            case 1:
                                EditShelf();
                                break;
                            case 2:
                                DisplayAllShelfs();
                                break;
                            case 3:
                                Console.Clear();
                                return;
                        }
                    }
                    // Wyświetla menu jęsli użytkownik jest adminem
                    else
                    {
                        Console.WriteLine("1. Edytuj półkę");
                        Console.WriteLine("2. Dodaj półkę");
                        Console.WriteLine("3. Usuń półkę");
                        Console.WriteLine("4. Wyświetl wszystkie półki");
                        Console.WriteLine("5. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 5))
                        {
                            case 1:
                                EditShelf();
                                break;
                            case 2:
                                AddShelf();
                                break;
                            case 3:
                                RemoveShelf();
                                break;
                            case 4:
                                DisplayAllShelfs();
                                break;
                            case 5:
                                Console.Clear();
                                return;
                        }
                    }
                }
            }

            // Metoda wywołująca menu sekcji
            private static void EditShelf()
            {
                Console.Clear();

                // Sprawdzenie obecność sekcji
                if (Library.shelfs.Count == 0)
                {
                    OnAction?.Invoke("Brak półki do edycji");
                    return;
                }
                // Wyświtela informację o sekcjach
                Library.DisplayAllShelfs();
                Console.WriteLine();
                Console.Write($"Wybierz półkę:");
                // Wywołanie menu magazynu
                ShelfsMenu.ShelfManager.ShelfMain(MainMenuManager.SelectNumber(1, Library.shelfs.Count) - 1);
            }
            // Metoda dodająca sekcję
            private static void AddShelf()
            {
                Console.Clear();
                Library.shelfs.Add(new Shelf(Library.shelfs.Count));
                Logger.SendLog("Dodał półkę");

                // Zdarzenie wyświetla wiadomość i kończy operację
                OnAction?.Invoke("Półka została dodana");
                
                return;
            }
            // Metoda usuwająca sekcję
            private static void RemoveShelf()
            {
                Console.Clear();

                // Sprawdzenia obecności sekcji
                if (Library.shelfs.Count == 0)
                {
                    OnAction?.Invoke("Brak półki do usunięcia");
                    return;
                }
                // Wyświetlanie wszystkich sekcji
                Library.DisplayAllShelfs();
                Console.WriteLine();

                Console.WriteLine($"Wybierz półkę do usunięcia: ");
                int selected_to_remove = MainMenuManager.SelectNumber(1, Library.shelfs.Count) - 1;
                Library.RemoveShelf(selected_to_remove);

                Logger.SendLog("Usunął półkę");
                // Zdarzenie wyświetla wiadomość i kończy operację
                OnAction?.Invoke($"Półka #{selected_to_remove + 1} była usunięta");
                return;
            }
            // Metoda wyświetlająca wszystkie sekcję
            private static void DisplayAllShelfs()
            {
                Console.Clear();
                Library.DisplayAllShelfs();
                // Zdarzenie wyświetla wiadomość i kończy operację
                OnAction?.Invoke("");
                return;
            }
            // Funkcja zwracająca indeks sekcji od użytkownika
            private static int SelectShelf()
            {
                // Sprawdza obecność sekcji
                if (Library.shelfs.Count == 0)
                {
                    Console.Clear();
                    OnAction?.Invoke("Brak półki do edycji");
                    return -1;
                }
                Console.WriteLine();
                Console.Write($"Wybierz półkę:");
                return MainMenuManager.SelectNumber(1, Library.shelfs.Count) - 1;
            }
        }
    }
}
