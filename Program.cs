using LibraryProject.Menus;
using LibraryProject.utils;

namespace LibraryProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Subskrypcja zdarzeń systemu logowania
            LogInSystem.OnLoginResult += UIHelper.EndLogin;
            LogInSystem.OnLoginResult += UIHelper.EndTask;

            // Subskrypcja zdarzeń menu głównego
            MainMenuManager.OnAction += UIHelper.ShowMessage;
            MainMenuManager.OnAction += UIHelper.EndTask;

            // Subskrypcja zdarzeń menu magazynu
            LibraryMenu.LibraryManager.OnAction += UIHelper.ShowMessage;
            LibraryMenu.LibraryManager.OnAction += UIHelper.EndTask;

            // Subskrypcja zdarzeń menu sekcji
            ShelfsMenu.ShelfManager.OnAction += UIHelper.ShowMessage;
            ShelfsMenu.ShelfManager.OnAction += UIHelper.EndTask;

            // Wywołanie logowania i menu głównego
            LogInMenu.LoginMenu();
            MainMenuManager.MainMenu();
        }

        
    }
}

