using System;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DatabaseAcces
    {
        private static DatabaseAcces instance;
        private string connectionString = @"Data Source=LAPTOP-PRO\MSSQLSERVER22;Initial Catalog=XeMay;Integrated Security=True";

        public static DatabaseAcces Instance
        {
            get
            {
                if (instance == null)
                    instance = new DatabaseAcces();
                return instance;
            }
        }

        private DatabaseAcces() { }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            int affectedRows = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                affectedRows = command.ExecuteNonQuery();

                connection.Close();
            }

            return affectedRows;
        }

        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                result = command.ExecuteScalar();

                connection.Close();
            }

            return result;
        }

        public int ExecuteStoredProcedure(string procName, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedure(procName, parameters, null, null);
        }

        public int ExecuteStoredProcedure(string procName, SqlParameter[] parameters, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            int affectedRows = 0;

            bool useExternalConnection = connection != null;

            if (!useExternalConnection)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }

            try
            {
                using (SqlCommand command = new SqlCommand(procName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    affectedRows = command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (!useExternalConnection && connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return affectedRows;
        }

        public DataTable ExecuteStoredProcedureWithReturn(string procName, params SqlParameter[] parameters)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(procName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }
    }
}