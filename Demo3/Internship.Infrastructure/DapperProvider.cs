using MySql.Data.MySqlClient;
using System;
using System.Data;
using Dapper;
using System.Collections.Generic;

namespace Internship.Infrastructure
{
    public class DapperProvider<T> where T : EntityBase
    {
        private readonly string constr = "server=localhost;uid=root;pwd=1111;database=tmainternship";

        //--
        public T QuerySingle(string query)
        {
            using var conn = new MySqlConnection(constr);
            try
            {
                var data = conn.QuerySingle<T>(query);
                return data;
            }
            catch (Exception)
            { }
            return null;
        }

        //--
        public IList<T> Query(string query)
        {
            using var conn = new MySqlConnection(constr);
            try
            {
                var data = conn.Query<T>(query).AsList();
                return data;
            }
            catch (Exception)
            { }
            return null;
        }

        //--
        public bool Excute(string query)
        {
            using var conn = new MySqlConnection(constr);
            try
            {
                var result = conn.Execute(query);
                return result > 0;
            }
            catch (Exception)
            { }
            return false;

        }

        //--
        public object ExecuteScalar(string query)
        {
            using var conn = new MySqlConnection(constr);
            try
            {
                var result = conn.ExecuteScalar(query);
                return result;
            }
            catch (Exception)
            { }
            return false;

        }
    }
}
