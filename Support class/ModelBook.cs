using EkzDemo.Support_class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkzDemo
{
  
    public partial class Book
    {
        public string CountInStoreStr { get; set; }
        public string CountInStokeStr { get; set; }
        public string Descript { get; set; }
        public string PathImage { get; set; }
        public string CostSale { get; set; }
        public string CountBook { get; set; }


        public static Book getBook(Book book)
        {
            if (book.CountInStock > 5)
            {
                book.CountInStokeStr = "много";
            }
            else if (book.CountInStock < 1)
            {
                book.CountInStokeStr = "нет";
            }
            else
            {
                book.CountInStokeStr = book.CountInStock.ToString();
            }
            if (book.CountInStore > 5)
            {
                book.CountInStoreStr = "много";
            }
            else if (book.CountInStore < 1)
            {
                book.CountInStoreStr = "нет";
            }
            else
            {
                book.CountInStoreStr = book.CountInStore.ToString();
            }
            book.Descript = "Описание: " + book.Description;
            book.PathImage = @"..\" + book.Image;
            return book;
        }
        public static List<Book> getListBook()
        {
            List<Book> books = BaseConnect.BaseModel.Book.ToList();
            List<Book> booksReturn = new List<Book>();
            foreach (Book book in books)
            {
                booksReturn.Add(getBook(book));
            }
            return booksReturn;
        }
    }
}
