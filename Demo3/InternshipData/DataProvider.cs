using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Internship.Data
{
    public class DataProvider
    {
        private readonly MySqlConnection _connection;

        public DataProvider(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        //--
        public DataTable ExecuteReader(string query)
        {
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            MySqlCommand _command = new(query, _connection);
            MySqlDataAdapter _adapter = new(_command);
            DataTable data = new();
            _adapter.Fill(data);

            _connection.Close();
            return data;
        }


        //--
        public bool ExecuteNonQuery(string query)
        {
            try
            {
                _connection.Open();
                MySqlCommand _command = new(query, _connection);
                int ok = _command.ExecuteNonQuery();

                _connection.Close();
                return ok != -1;
            }
            catch (Exception)
            {
                _connection.Close();
                return false;
            }
        }

        //--
        public object ExecuteScalar(string query)
        {
            _connection.Open();
            MySqlCommand _command = new(query, _connection);
            object data = _command.ExecuteScalar();

            _connection.Close();
            return data;
        }
    }
}
