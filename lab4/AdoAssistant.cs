using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab4
{
    // Клас доступу до БД
    public class AdoAssistant
    {
        // Отримуємо рядок з'єднання з файлу App.config
        String connectionString = System.Configuration.
        ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;
        //************************************************ *********
        // Метод читання даних з DataTable
        //************************************************ *********
        DataTable dt = null; // Посилання на об'єкт DataTable
        public DataTable TableLoad()
        {
            if (dt != null) return dt; // Завантажимо таблицю лише один раз
                                       // Заповнюємо об'єкт таблиці даними з БД
            dt = new DataTable();
            // Створюємо об'єкт підключення
            using (SqlConnection сonnection = new SqlConnection(connectionString))
            {
                SqlCommand command = сonnection.CreateCommand(); // Створюємо об'єкт команди
                SqlDataAdapter adapter = new SqlDataAdapter(command); // Створюємо об'єкт читання
                                                                      //Завантажує дані 
                command.CommandText =
                    "SELECT ISBN, Name, Authors, Publisher, Year_of_publication FROM Books";
                try
                {
                    // Метод сам відкриває БД і сам її закриває
                    adapter.Fill(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка підключення до БД");
                }
            }
            return dt;
        }
        // Додавання запису
        public void CreateBook(int isbn, string name,
            string authors, string publisher, string year)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText =
                    "INSERT INTO Books VALUES (@ISBN, @Name, @Authors, @Publisher, @Year)";

                command.Parameters.AddWithValue("@ISBN", isbn);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Authors", authors);
                command.Parameters.AddWithValue("@Publisher", publisher);
                command.Parameters.AddWithValue("@Year", year);

                command.ExecuteNonQuery();
            }
        }
        // Оновлення запису
        public void UpdateBook(int isbn, string name,
            string authors, string publisher, string year)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText =
                    "UPDATE Books SET " +
                    "Name=@Name, " +
                    "Authors=@Authors, " +
                    "Publisher=@Publisher, " +
                    "Year_of_publication=@Year " +
                    "WHERE ISBN=@ISBN";

                command.Parameters.AddWithValue("@ISBN", isbn);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Authors", authors);
                command.Parameters.AddWithValue("@Publisher", publisher);
                command.Parameters.AddWithValue("@Year", year);

                command.ExecuteNonQuery();
            }
        }
        // Видалення запису
        public void DeleteBook(int isbn)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText =
                    "DELETE FROM Books WHERE ISBN=@ISBN";

                command.Parameters.AddWithValue("@ISBN", isbn);

                command.ExecuteNonQuery();
            }
        }
    }
}