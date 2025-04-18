namespace LibraryProject.Models.Users
{
    public enum Role
    {
        Admin,
        Manager,
        Client
    }
    public class User
    {
        public string UserName { get; private set; }
        public Role Role { get; private set; }

        // Przypisuje podstawowe wartości przy tworzeniu
        public User(string userName, Role role)
        {
            UserName = userName;
            Role = role;
        }
    }
}
