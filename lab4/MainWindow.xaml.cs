using System.Data;
using System.Windows;
using System.Xml.Linq;

namespace lab4
{
    public partial class MainWindow : Window
    {
        AdoAssistant ado = new AdoAssistant();

        public MainWindow()
        {
            InitializeComponent();
            GetDataContext();
        }

        private void GetDataContext()
        {
            DataTable dt = ado.TableLoad();
            list.ItemsSource = dt.DefaultView;
        }

        // CREATE
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ado.CreateBook(
                int.Parse(txtISBN.Text),
                txtName.Text,
                txtAuthors.Text,
                txtPublisher.Text,
                txtYear.Text
            );

            GetDataContext();
        }

        // UPDATE
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ado.UpdateBook(
                int.Parse(txtISBN.Text),
                txtName.Text,
                txtAuthors.Text,
                txtPublisher.Text,
                txtYear.Text
            );

            GetDataContext();
        }

        // DELETE
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ado.DeleteBook(
                int.Parse(txtISBN.Text)
            );

            GetDataContext();
        }
    }
}
