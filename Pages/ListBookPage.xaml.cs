using EkzDemo.Support_class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EkzDemo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListBookPage.xaml
    /// </summary>
    public partial class ListBookPage : Page
    {
        List<Book> Busket = new List<Book>();
        List<Book> Books = Book.getListBook();
        public ListBookPage()
        {
            InitializeComponent();
            ListBookListBox.ItemsSource = Books;
        }
        public ListBookPage(List<Book> Busket)
        {
            InitializeComponent();
            ListBookListBox.ItemsSource = Books;
            this.Busket = Busket;
            CountSelectBook.Text = Busket.Count().ToString();
            double cost = 0;
            foreach (Book book1 in Busket)
            {
                cost = cost + Convert.ToDouble(book1.Cost);
            }
            double Sale = DLL.DLL.SaleCost(Busket.Count, cost);
            СostSaleSelectBook.Text = " " + (cost - (cost * Sale)).ToString();
            СostSelectBook.Text = cost.ToString();
            СostSelectBook.Visibility = Visibility.Visible;
            SaleProcentBook.Text = (Sale * 100).ToString();
            if (Busket.Count == 0)
            {
                BaseConnect.BaseModel = new EkzDemoEntities();
                Books = Book.getListBook();
                ListBookListBox.ItemsSource = Books;
                ListBookListBox.Items.Refresh(); 
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            int id = Convert.ToInt32(senderButton.Uid);
            Busket.Add(Books.FirstOrDefault(x => x.id == id));
            Book book = Books.FirstOrDefault(x => x.id == id);
            ListBookListBox.ItemsSource = Books;
            if (book.CountInStore > 0)
                book.CountInStore--;
            else if (book.CountInStock > 0)
                book.CountInStock--;
            else MessageBox.Show("Нет в наличии!");
            book = Book.getBook(book);
            ListBookListBox.ItemsSource = Books;
            CountSelectBook.Text = Busket.Count().ToString();
            double cost = 0;
            foreach (Book book1 in Busket)
            {
                cost = cost + Convert.ToDouble(book1.Cost);
            }
            double Sale = DLL.DLL.SaleCost(Busket.Count, cost);
            СostSaleSelectBook.Text = " " + (cost - (cost * Sale)).ToString();
            СostSelectBook.Text = cost.ToString();
            СostSelectBook.Visibility = Visibility.Visible;
            ListBookListBox.Items.Refresh();
            SaleProcentBook.Text = (Sale * 100).ToString();
        }

        private void GoBucket_Click(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.Navigate(new BusketPage(Busket));
        }
    }
}
