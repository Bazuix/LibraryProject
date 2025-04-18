namespace LibraryProject.utils
{
    // Klasa odpowiadająca za przypisanie logów
    public static class Logger
    {
        // Metoda zapisująca logi
        public static void SendLog(string action)
        {
            // Tworzy log, który będzie dodany do pliku. Wartością jest data i cza oraz akcja, wykonana przez użytkownika
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string message = $"[{time}] {LogInSystem.user?.UserName} {action}";
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");
            string filepath = Path.Combine(folderPath, "logs.txt");
            // Tworzy plik jesłi on nie istnieje
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }

            // Zapisuje nowy log do pliku
            File.AppendAllText(filepath,message + Environment.NewLine);
        }
    }
}
