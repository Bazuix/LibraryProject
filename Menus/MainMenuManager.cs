
namespace LibraryProject.Menus
{
    // Menu główne
    public class MainMenuManager
    {
        public delegate void MenuAction(string message);

        //Zdarzenie wywołane po zakończeniu operacji
        public static event MenuAction? OnAction;
        public static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Główne Menu:");
                Console.WriteLine("1. Zarządzanie biblioteką");
                Console.WriteLine("2. Zapisz dane do pliku");
                Console.WriteLine("3. Wczytaj dane z pliku");
                Console.WriteLine("4. Wyjdź z programu");

                switch (SelectNumber(1, 4))
                {
                    case 1:
                        Console.Clear();
                        LibraryMenu.LibraryManager.LibraryMain();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Zapis danych do pliku...");
                        DataSystem.SaveData();
                        OnAction?.Invoke("");
                        MainMenu();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Wczytywanie danych z pliku...");
                        DataSystem.GetData();
                        OnAction?.Invoke("");
                        MainMenu();
                        break;
                    case 4:
                        Console.WriteLine("Program się kończy...");
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            }

        }
        // Funkcja zwracająca liczbę, pobraną od użytkownika
        public static int SelectNumber(int min, int max)
        {
            int selected;
            while (true)
            {
                string range = $"({min}-{max})";
                if (min == max)
                {
                    range = $"({min.ToString()})";
                }
                Console.Write($"Wybierz opcję {range}: ");

                // Sprawsza czy podana liczba wykonuje wszystkie kryteria
                if (int.TryParse(Console.ReadLine(), out selected) && selected >= min && selected <= max)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Wpisana nieprawidłowa wartość. Liczba musi być w przedziale: {min}-{max}");
                }
            }
            return selected;
        }
        // Funkcja zwracająca tekst, pobrany od użytkownika 
        public static string SelectText(string text)
        {
            string result;
            do
            {
                Console.Write(text);
                result = Console.ReadLine() ?? "";
            } while (string.IsNullOrWhiteSpace(result) || int.TryParse(result, out _));
            return result;
        }
    }


   
    
}
