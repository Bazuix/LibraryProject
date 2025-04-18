using LibraryProject.utils;

namespace LibraryProject.Menus
{
    // Menu logowania
   public class LogInMenu
    {
        public static void LoginMenu()
        {
            while (true)
            {
                Console.WriteLine("Witaj w systemie zarządzania biblioteką!");
                if (LogInSystem.LogIn())
                {
                    Console.Clear();
                    return;
                }
            }
        }
    }
}
