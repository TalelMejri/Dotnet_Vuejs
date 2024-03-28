using MySqlConnector;
using System.Data.SqlClient;

namespace Backend.Service
{
    public class RequeteService
    {
        private readonly string ConnectionString;
        private readonly string _requete;
        private readonly string _output;
        public RequeteService(string conn,string req,string output)
        {
            ConnectionString = conn;
            _requete = req;
            _output= output;
        }

        public string ExecuterRequetMysql()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand(_requete, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)

                                    using (StreamWriter writer = new StreamWriter(_output))
                                    {
                                        while (reader.Read())
                                        {
                                            string rowData = $"{reader["id"]}, {reader["name"]}, {reader["photo"]}, {reader["status"]}";
                                            writer.WriteLine(rowData);
                                        }
                                        return "success";
                                    }
                             
                                else
                                {
                                    return "No rows found";
                               
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return " Error ";
                    }
                }

            }
            catch (Exception ex)
            {
                return " Error ";

            }
        }

        public string ExecuterRequetSqlServer()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_requete, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (StreamWriter writer = new StreamWriter(_output))
                            {
                                while (reader.Read())
                                {
                                    string rowData = $"{reader["FileName"]}, {reader["id"]}";
                                    writer.WriteLine(rowData);
                                }
                                return "success";
                            }
                        }
                        else
                        {
                            return "No rows found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
    }
}
