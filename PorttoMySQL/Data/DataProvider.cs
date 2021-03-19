using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace WebApplication.Data
{    static class DataProvider
    {
        // lớp này xử lí việc kết nối tới SQL Server và thực thi truy vấn
        public static readonly string connectionStr = @"Server=127.0.0.1; port= 3306;
                                                        Database=demoproduct;
                                                        UserId=root;
                                                        Password=1111;";

        static readonly MySqlConnection connection = new(connectionStr);
        static MySqlDataAdapter adapter;
        static MySqlCommand command;

        //--
        public static DataSet ExecuteReader(string query)
        {
            connection.Open();

            command = new MySqlCommand(query, connection);
            adapter = new MySqlDataAdapter(command);
            DataSet data = new();
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
                command.ExecuteNonQuery();

                connection.Close();
                return true;
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
