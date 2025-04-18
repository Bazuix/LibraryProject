using LibraryProject.Models.Library;
using LibraryProject.Models.Users;
using System;
using System.IO;

namespace LibraryProject.Menus
{
    class DataSystem
    {
        //Metoda do zapisywania danych do pliku
        public static void SaveData()
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");

            string filepath = Path.Combine(folderPath, "library.txt");

            // Upewnij się, że folder istnieje
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filepath))
            {
                Console.WriteLine("Plik został stworzony");
                using (FileStream fs = File.Create(filepath))
                {
                    // Zamknięcie FileStream po stworzeniu pliku
                }
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    Console.WriteLine("Zapisywanie danych do pliku...");
                    writer.WriteLine($"Półka count: {Library.shelfs.Count}");

                    for (int i = 0; i < Library.shelfs.Count; i++)
                    {
                        writer.WriteLine($"Półka: {i}");
                        writer.WriteLine($"Książki Count: {Library.shelfs[i].Books.Count}");
                        for (int j = 0; j < Library.shelfs[i].Books.Count; j++)
                        {
                            Books.Book book = Library.shelfs[i].Books[j];
                            string type = book is Books.BookFantasy ? "Fantasy" : "Thriller";
                            writer.WriteLine($"Książki: {j}, {type}, {book.count}, {book.objectsName}");
                        }
                    }
                }

                Console.WriteLine("Dane zostały zapisane");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy zapisie do pliku: {ex.Message}");
            }
        }

        //bool sprawdzające poprawność danych
        private static bool CheckData(string filepath)
        {
            bool result = true;

            try
            {
                string[] allLines = File.ReadAllLines(filepath);
                for (int i = 0; i < allLines.Length; i++)
                {
                    if (allLines[i].StartsWith("Półka:"))
                    {
                        int shelf = int.Parse(allLines[i].Split(":")[1]);
                        if (int.Parse(allLines[i + 1].Split(": ")[1]) < 0)
                        {
                            throw new ArgumentException();
                        }
                        for (int j = 0; j < int.Parse(allLines[i + 1].Split(": ")[1]); j++)
                        {
                            string[] BooksInfo = allLines[i + j + 2].Split(": ")[1].Split(", ");
                            if (BooksInfo.Length < 4)
                            {
                                throw new InvalidDataException();
                            }
                            int id = int.Parse(BooksInfo[0]);
                            if (id < 0 || id > int.Parse(allLines[i + 1].Split(": ")[1]))
                            {
                                throw new ArgumentException();
                            }
                            string type = BooksInfo[1].Trim();
                            if (type != "Fantasy" && type != "Thriller")
                            {
                                throw new ArgumentException();
                            }
                            int count = int.Parse(BooksInfo[2]);
                            if (count <= 0)
                            {
                                throw new ArgumentException();
                            }
                            string objectsName = BooksInfo[3];
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Brak pliku");
                Console.ReadKey();
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Błąd przy odczytu pliku: {ex.Message}");
                Console.ReadKey();
                return false;
            }
            catch (FormatException)
            {
                Console.WriteLine("Nie prawidłowy format");
                Console.ReadKey();
                return false;
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Uszkodzone dane");
                Console.ReadKey();
                return false;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Uszkodzone dane");
                Console.ReadKey();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex}");
                Console.ReadKey();
                return false;
            }
            return result;
        }

        //Metoda wczytująca dane
        public static void GetData()
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");

            string filepath = Path.Combine(folderPath, "library.txt");

            if (CheckData(filepath))
            {
                string[] allLines = File.ReadAllLines(filepath);
                for (int i = 0; i < allLines.Length; i++)
                {
                    if (allLines[i].StartsWith("Półka:"))
                    {
                        Library.shelfs.Add(new Shelf(Library.shelfs.Count));
                        int shelf = int.Parse(allLines[i].Split(":")[1]);
                        for (int j = 0; j < int.Parse(allLines[i + 1].Split(": ")[1]); j++)
                        {
                            string[] BooksInfo = allLines[i + j + 2].Split(": ")[1].Split(", ");
                            int id = int.Parse(BooksInfo[0]);
                            string type = BooksInfo[1].Trim();
                            int count = int.Parse(BooksInfo[2]);
                            string objectsName = BooksInfo[3];
                            Books.Book book;
                            User owner = new User("Unknown", Role.Client); // Zastąp "Unknown" odpowiednią wartością
                            if (type == "Fantasy")
                            {
                                book = new Books.BookFantasy(id, shelf, count, objectsName, owner);
                            }
                            else
                            {
                                book = new Books.BookThriller(id, shelf, count, objectsName, owner);
                            }
                            Library.shelfs[shelf].AddBook(book);
                        }
                    }
                }
                Console.WriteLine("Dane zostały wczytane!");
            }
        }
    }
}
