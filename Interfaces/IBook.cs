namespace LibraryProject.Interfaces
{
 
    public interface IBook
    {
        void ShowInfo();
        void ChangeInfo(string objectsName, int count);
        void MoveBook(int id_shelf);
    }
}
