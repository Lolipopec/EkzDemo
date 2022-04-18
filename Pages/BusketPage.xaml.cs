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
    /// Логика взаимодействия для BusketPage.xaml
    /// </summary>
    public partial class BusketPage : Page
    {
        double Sale = 0;
        List<Book> Busket;
        public BusketPage(List<Book> busket)
        {
            InitializeComponent();
            Busket = busket;
            double cost = 0;
            int Count = 0;
            foreach (Book book1 in busket)
            {
                Count += Convert.ToInt32(book1.CountBook);
                cost = cost + Convert.ToDouble(book1.Cost)* Count;
            }
            Sale = DLL.DLL.SaleCost(Count, cost);
            List<Book> busketInList = new List<Book>();
            foreach (Book book in busket.Distinct().ToList())
            {
                book.CostSale = " " + (Math.Floor(Convert.ToDouble(book.Cost) - (Convert.ToDouble(book.Cost) * Sale))).ToString();
                busketInList.Add(book);
            }
            BusketListBox.ItemsSource = busketInList;
            Busket = busketInList;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.Navigate(new ListBookPage(Busket));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Busket.Clear();
            BusketListBox.ItemsSource = Busket;
            BusketListBox.Items.Refresh();
        }

        private void DeleteBookInBusket_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            Busket.Remove(Busket.FirstOrDefault(x => x.id == id));
            BusketListBox.Items.Refresh();
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            int numOrder = 0;
            int countBooks = 0;
            double CountCost = 0;
            if (BaseConnect.BaseModel.Order.Count() > 0)
            {
                numOrder = BaseConnect.BaseModel.Order.Max(x => x.NumberOrder) + 1;
            }
            else numOrder = 1;
            foreach (Book book in Busket)
            {
                Order order = new Order();
                if (book.CountInStore < Convert.ToInt32(book.CountBook))
                {
                    flag = true;
                    
                    int minus = book.CountInStore - Convert.ToInt32(book.CountBook);
                    book.CountInStore = 0;
                    book.CountInStock -= minus * -1;
                }
                else
                {
                    book.CountInStock -= Convert.ToInt32(book.CountBook);
                }
                order.IdBook = book.id;
                order.Count = Convert.ToInt32(book.CountBook);
                order.DateOrder = DateTime.Now;
                countBooks += Convert.ToInt32(book.CountBook);
                CountCost += Convert.ToDouble(book.Cost) * Convert.ToDouble(book.CountBook);
                if (BaseConnect.BaseModel.Order.Count() > 0)
                {
                    order.NumberOrder = numOrder;
                }
                else order.NumberOrder = 1;
                BaseConnect.BaseModel.Order.Add(order);
            }
            Sale = DLL.DLL.SaleCost(countBooks, CountCost);
            if (Sale > 1)
                Sale = 1;
            if (flag)
                MessageBox.Show("Номер заказ: " + numOrder + "\n Заказ можно забрать: " + DateTime.Now.AddDays(7).ToShortDateString() + "\n Общее количество книг: " + countBooks + "\nИтоговая цена:" + Math.Floor(CountCost - (CountCost * Sale)) + "\nСкидка:" + Sale * 100);
            else
            {
                MessageBox.Show("Номер заказ: " + numOrder + "\n Заказ можно забрать: " + DateTime.Now.ToShortDateString() + "\n Общее количество книг: " + countBooks + "\nИтоговая цена:" + Math.Floor(CountCost - (CountCost * Sale)) + "\nСкидка:" + Sale * 100);
            }
            BaseConnect.BaseModel.SaveChanges();
            Busket.Clear();
            BusketListBox.ItemsSource = Busket;
            BusketListBox.Items.Refresh();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                double cost = 0;
                int Count = 0;
                int id = Convert.ToInt32(textBox.Uid);
                Book book2 = BaseConnect.BaseModel.Book.FirstOrDefault(x => x.id == id);
                if (Convert.ToInt32(book2.CountInStock)+ Convert.ToInt32(book2.CountInStore) > Convert.ToInt32(textBox.Text))
                {
                    foreach (Book book1 in Busket)
                    {
                        if (book1.id == id)
                        {
                            book1.CountBook = textBox.Text;
                        }
                        cost = cost + (Convert.ToDouble(book1.Cost) * Convert.ToDouble(book1.CountBook));
                    }
                    foreach (Book book in Busket.Distinct().ToList())
                    {
                        Count += Convert.ToInt32(book.CountBook);
                    }
                    Sale = DLL.DLL.SaleCost(Count, cost);
                    if (Sale > 1)
                        Sale = 1;
                    List<Book> busketInList = new List<Book>();
                    foreach (Book book in Busket.Distinct().ToList())
                    {
                        book.CostSale = " " + (Math.Floor(Convert.ToDouble(book.Cost) - (Convert.ToDouble(book.Cost) * Sale))).ToString();
                        if (book.id == id)
                            book.CountBook = textBox.Text;
                        busketInList.Add(book);
                    }
                    BusketListBox.ItemsSource = busketInList;
                    Busket = busketInList;
                    BusketListBox.Items.Refresh();
                }
                else MessageBox.Show("Такого количества нет");
            }
            catch
            {
                
            }
        }
    }
}
