namespace LibraryProject.utils
{
    public  static class UIHelper
    {
        // Metoda kończąca operację – oczyszcza konsolę po naciśnięciu klawisza
        public static void EndTask(string message)
        {
            Console.WriteLine("Naciśnij dowolny klawisz aby kontynuować.");
            Console.ReadKey();
            Console.Clear();
        }
        // Metody wyświetlająca status logowania
        public static void EndLogin(string message)
        {
            Console.Clear();
            Console.WriteLine($"Status logowania: {message}");
        }
        // Metoda wyświetlająca wiadomość
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
