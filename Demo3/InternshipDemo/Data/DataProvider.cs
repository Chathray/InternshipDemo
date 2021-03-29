using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace WebApplication.Data
{
    static class DataProvider
    {
        // lớp này xử lí việc kết nối tới SQL Server và thực thi truy vấn
        public static readonly string connectionStr = "server=localhost;uid=root;pwd=1111;database=tmainternship";

        static readonly MySqlConnection conn = new(connectionStr);
        static MySqlDataAdapter adapter;
        static MySqlCommand command;

        //--
        public static DataTable ExecuteReader(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                command = new MySqlCommand(query, conn);
                adapter = new MySqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }
        }

        public static DataSet ExecuteReaders(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                command = new MySqlCommand(query, conn);
                adapter = new MySqlDataAdapter(command);
                DataSet data = new DataSet();
                adapter.Fill(data);
                return data;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }
        }


        //--
        public static bool ExecuteNonQuery(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                command = new MySqlCommand(query, conn);
                int ok = command.ExecuteNonQuery();

                conn.Close();
                return ok != -1;
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }

        //--
        public static object ExecuteScalar(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                command = new MySqlCommand(query, conn);
                return command.ExecuteScalar();
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }
        }
    }
}
