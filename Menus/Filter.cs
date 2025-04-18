using LibraryProject.Models.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryProject.Models.Library.Books;

namespace LibraryProject.Menus
{
    public class Filter
    {
        public static List<Book> FilterByTitle(string title)
        {
            return Library.shelfs.SelectMany(s => s.Books)
                                 .Where(b => b.objectsName.Contains(title, StringComparison.OrdinalIgnoreCase))
                                 .ToList();
        }

        public static List<Book> FilterByType(string typeName)
        {
            return Library.shelfs.SelectMany(s => s.Books)
                                 .Where(b => b.GetType().Name.Contains(typeName, StringComparison.OrdinalIgnoreCase))
                                 .ToList();
        }
        public static List<Book> FilterByPageCount(int pages)
        {
            return Library.shelfs
                .SelectMany(s => s.Books)
                .Where(b => b.count == pages)
                .ToList();
        }


        public static void DisplayBooks(List<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"[{book.GetType().Name}] {book.objectsName}, ilość stron: {book.count}");
            }
        }
    }
}
