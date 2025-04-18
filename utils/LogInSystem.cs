using LibraryProject.Models.Users;
using static LibraryProject.utils.PasswordHash;
namespace LibraryProject.utils
{
    // Klasa z systemem logowania użytkownika
    public static class LogInSystem
    {
        public delegate void LoginAction(string message);

        // Zdarzenie wywołane po probie logowania
        public static event LoginAction? OnLoginResult;

        //zalogowany użytkownik
        public static User? user{ get; private set; }



        // Wyjątek rzucany w przpadki niprawidłowego loginu lub hasła
        public class AuthenticationFailedException : Exception
        {
            public AuthenticationFailedException() : base("Nieprawidłowy login lub hasło") { }
        }
        // Wyjątek rzucany w przypadku pustego logina lub hasła
        public class EmptyCredentialsException: Exception
        {
            public EmptyCredentialsException() : base("Login lub hasło nie może być puste") { }
        }

        // Metoda logowania
        public static bool LogIn()
        {
            // Pobieranie logina i hasła od użytkownika
            Console.Write("Podaj UserName:");
            string username = Console.ReadLine() ?? "";
            Console.Write("Podaj Password:");
            string password = Console.ReadLine() ?? "";

            try
            {
                // Sprawdzenie czy jejst wartość pusta
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    throw new EmptyCredentialsException();
                }
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");
                string filePath = Path.Combine(folderPath, "users.txt");
             

                // Sprawdzenie czy plik istnieje
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Plik {filePath} nie został znaleziony");
                }

                // Zbiera wszystkie linijki z pliku
                string[] allLines = File.ReadAllLines(filePath);

                
                for (int i = 0; i < allLines.Length; i++)
                {
                    // Sprawdzenie czy linijka ma login
                    if (allLines[i].Split(": ").First() == "login" && allLines[i].Split(": ")[1].Trim() == username)
                    {
                        // Sprawdzenie czy hasło jest prawidłowe
                        if (PasswordHash.VerifyPassword(password, allLines[i + 1].Split(": ")[1]))

                            {
                            // Wyczytuje role użytkownika
                            string rolePart = allLines[i + 2].Split(": ")[1].Trim();
                            Role role;

                            //Przypisuje role użytkownika
                            if( rolePart == "Admin")
                            {
                                role = Role.Admin;
                            }
                            else if (rolePart == "Manager")
                            {
                                role = Role.Manager;
                            }
                            else
                            {
                                role = Role.Client;
                            }

                            // Zdarzenie OnLoginResult pokazuje status logowania i kończy operację
                            OnLoginResult?.Invoke("Udane");

                            // Przypisuje wartości zalogowanego użytkownika
                            user = new User(username, role);
                            return true;
                        }
                        else
                        {
                            throw new AuthenticationFailedException();
                        }
                    }
                }
                throw new AuthenticationFailedException();
            }
            catch (Exception ex) when (ex is AuthenticationFailedException || ex is EmptyCredentialsException || ex is FileNotFoundException)
            {
                //Wywołuje wiadomość o błędzie i zaczyna logowanie od początki
                Console.WriteLine(ex.Message);
                OnLoginResult?.Invoke("Nieudane");
                return false;
            }
        }
    }
}
