using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace WebApplication.Data
{
    static class DataProvider
    {
        // lớp này xử lí việc kết nối tới SQL Server và thực thi truy vấn
        public static readonly string connectionStr = @"user id=root;server=localhost;database=demoproduct;
                                                        Password=1111;";

        static readonly MySqlConnection connection = new(connectionStr);
        static MySqlDataAdapter adapter;
        static MySqlCommand command;

        //--
        public static DataTable ExecuteReader(string query)
        {
            connection.Close();
            connection.Open();

            command = new MySqlCommand(query, connection);
            adapter = new MySqlDataAdapter(command);
            DataTable data = new DataTable();
            adapter.Fill(data);

            connection.Close();
            return data;

        }


        //--
        public static bool ExecuteNonQuery(string query)
        {
            try
            {
                connection.Open();
                command = new MySqlCommand(query, connection);
                int ok = command.ExecuteNonQuery();

                connection.Close();
                return ok != -1;
            }
            catch (Exception)
            {
                connection.Close();
                return false;
            }
        }

        //--
        public static object ExecuteScalar(string query)
        {
            connection.Open();
            command = new MySqlCommand(query, connection);
            object data = command.ExecuteScalar();

            connection.Close();
            return data;
        }
    }
}
