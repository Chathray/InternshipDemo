using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Internship.Infrastructure
{
    public class DataProvider
    {
        private readonly MySqlConnection conn;

        public DataProvider(MySqlConnection connection)
        {
            conn = connection;
        }

        //--
        public DataTable ExecuteReader(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                MySqlCommand command = new(query, conn);
                MySqlDataAdapter adapter = new(command);
                DataTable data = new();
                adapter.Fill(data);
                return data;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }
        }

        public DataSet ExecuteReaders(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                MySqlCommand command = new(query, conn);
                MySqlDataAdapter adapter = new(command);
                DataSet data = new();
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
        public bool ExecuteNonQuery(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                MySqlCommand command = new(query, conn);
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
        public object ExecuteScalar(string query)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                MySqlCommand command = new(query, conn);
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
