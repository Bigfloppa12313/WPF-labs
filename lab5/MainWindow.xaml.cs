using System.Linq;
using System.Windows;

namespace lab5
{
    public partial class MainWindow : Window
    {
        DBTest2Entities db = new DBTest2Entities();

        public MainWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            BooksGrid.ItemsSource = db.Books.ToList();

            PublishersGrid.ItemsSource = db.Publishers.ToList();
        }

        private void ShowBooksAfter2020_Click(object sender, RoutedEventArgs e)
        {
            var books = db.Books
                          .Where(b => b.YearOfPublication > 2016)
                          .ToList();

            BooksAfter2020Grid.ItemsSource = books;
        }

        private void CountBooks_Click(object sender, RoutedEventArgs e)
        {
            int count = db.Books.Count();

            CountText.Text = "Books count: " + count;
        }

        private void ExecuteSql_Click(object sender, RoutedEventArgs e)
        {
            var books = db.Books
                          .SqlQuery("SELECT * FROM Books WHERE YearOfPublication = 1988")
                          .ToList();

            SqlGrid.ItemsSource = books;
        }
    }
}